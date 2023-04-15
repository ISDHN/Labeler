using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Labeler.Interop;
using static Labeler.Interop.Interop;
namespace Labeler
{
	public struct labelformat
	{
		public string text;
		public ulong inpoint;
		public ulong outpoint;
	}
	public partial class Form1 : Form
	{
		private ulong inpoint;
		private ulong outpoint;
		private ulong position;
		private bool playing;
		private string path;
		private IMFMediaSession session;
		private IMFMediaSource source;
		private IMFPresentationClock clock;
		private Thread Mediabump;
        private XmlSerializer serializer = new XmlSerializer(typeof(List<labelformat>));
        private List<labelformat> labels = new List<labelformat>();
		public Form1()
		{
			CheckForIllegalCrossThreadCalls = false;
			InitializeComponent();
			MFStartup(MF_VERSION,MFSTARTUP_FULL);
			MFCreateMediaSession(IntPtr.Zero, out session);
			Mediabump = new Thread(ProcessMessage);
		}

		private void SetIn_Click(object sender, EventArgs e)
		{
			inpoint = position;
			InPointLabel.Text = (inpoint / 1e7).ToString();
		}

		private void ToIn_Click(object sender, EventArgs e)
		{
			ChangePosition(inpoint);
			if (!playing) session.Pause();
		}

		private void SetOut_Click(object sender, EventArgs e)
		{
			outpoint= position;
            OutPointLabel.Text = (outpoint / 1e7).ToString();
        }

		private void ToOut_Click(object sender, EventArgs e)
		{
			ChangePosition(outpoint);
			if (!playing) session.Pause();
		}

		private void Form1_DragDrop(object sender, DragEventArgs e)
		{
			string file = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
			if (File.Exists(file) && file.Substring(file.Length - 4) == ".aac")
			{
				MFCreateTopology(out IMFTopology topo);
				MFCreateSourceResolver(out IMFSourceResolver resolver);
				resolver.CreateObjectFromURL(file, MF_RESOLUTION_MEDIASOURCE | MF_RESOLUTION_CONTENT_DOES_NOT_HAVE_TO_MATCH_EXTENSION_OR_MIME_TYPE, null, out _, out IUnknown unknown);
				source = unknown as IMFMediaSource;
				source.CreatePresentationDescriptor(out IMFPresentationDescriptor pd);
				pd.GetStreamDescriptorByIndex(0, out _, out IMFStreamDescriptor sd);
				MFCreateAudioRendererActivate(out IMFActivate arender);
				MFCreateTopologyNode(MF_TOPOLOGY_SOURCESTREAM_NODE, out IMFTopologyNode sourcenode);
				sourcenode.SetUnknown(MF_TOPONODE_SOURCE, source as IUnknown);
				sourcenode.SetUnknown(MF_TOPONODE_PRESENTATION_DESCRIPTOR, pd as IUnknown);
				sourcenode.SetUnknown(MF_TOPONODE_STREAM_DESCRIPTOR, sd as IUnknown);
				topo.AddNode(sourcenode);
				MFCreateTopologyNode(MF_TOPOLOGY_OUTPUT_NODE, out IMFTopologyNode outputnode);
				outputnode.SetObject(arender as IUnknown);
				topo.AddNode(outputnode);
				sourcenode.ConnectOutput(0, outputnode, 0);
				try
				{
					Mediabump.Start();
				}
				catch { 
				}
				session.SetTopology(0, topo);				
				pd.GetUINT64(MF_PD_DURATION, out ulong duration);
				Progress.Maximum = (int)(duration/1e7);
				path = file;
				Play.Enabled = true;
				Forward.Enabled = true;
				Backward.Enabled = true;
				ToIn.Enabled = true;
				ToOut.Enabled = true;
				Progress.Enabled = true;
			}
		}

		private void SentenceList_DragDrop(object sender, DragEventArgs e)
		{
			string file = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
			if (File.Exists(file) && file.Substring(file.Length - 4) == ".xml")
			{
				try
				{
					Stream stream = File.Open(file, FileMode.Open);
                    labels = (List<labelformat>)serializer.Deserialize(stream);
					foreach(labelformat label in labels) {
						SentenceList.Items.Add(label.text);
					}
					stream.Close();
				}
				catch { 
				}
			}
		}

		private void Form1_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))           
				e.Effect = DragDropEffects.All;           
			else
				e.Effect = DragDropEffects.None;
		}

		private void SentenceList_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.All;
			else
				e.Effect = DragDropEffects.None;
		}
		private void ChangePosition(ulong pos)
		{
			position = pos;
			try
			{
				Progress.Value = (int)(pos / 1e7);
				PropVariant prop = new PropVariant()
				{
					vt = (ushort)VarEnum.VT_I8,
					unionmember = pos
				};
				session.Start(Guid.Empty, prop);
			}
			catch {
			}
		}

		private void Add_Click(object sender, EventArgs e)
		{
			labelformat record = new labelformat()
			{
				text = Textbox.Text,
				inpoint = inpoint,
				outpoint = outpoint,
			};
			labels.Add(record);
			SentenceList.Items.Add(Textbox.Text);
			Textbox.Text = "";
		}

		private void Forward_Click(object sender, EventArgs e)
		{
			ChangePosition(position + 25000000);
			if(!playing) session.Pause();
		}

		private void Backward_Click(object sender, EventArgs e)
		{
			ChangePosition(position - 25000000);
			if(!playing) session.Pause();
		}

		private void Play_Click(object sender, EventArgs e)
		{		
			if (!playing)
			{
				Play.Text = "⏸";
				ChangePosition(position);
			}
			else
			{
				Play.Text = "▶";
				session.Pause();
			}
            playing = !playing;
        }
		private void ProcessMessage()
		{
			while(session != null)
			{
                int hr = session.GetEvent(1, out IMFMediaEvent mediaevent);
                if (hr == 0 && mediaevent != null)
                {
                    mediaevent.GetType(out uint eventtype);
                    mediaevent = null;
					switch (eventtype)
					{
						case MESessionStarted:
							session.GetClock(out IMFClock _clk);
							clock = _clk as IMFPresentationClock;
							break;
					}
                }
				if (clock != null && playing)
				{
					clock.GetTime(out position);
					Progress.Value = (int)(position / 1e7);
				}
            }
		}

        private void Progress_Scroll(object sender, EventArgs e)
        {
			ChangePosition((ulong)(Progress.Value * 1e7));
            if (!playing) session.Pause();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
			
            if (e.KeyCode == Keys.Enter)
            {
				Add.PerformClick();
                e.Handled = true;
            }
			else if (e.KeyCode == Keys.I && e.Control)
			{
				SetIn.PerformClick();
                e.Handled = true;
            }
			else if (e.KeyCode == Keys.O && e.Control)
			{
				SetOut.PerformClick();
                e.Handled = true;
            }
			else if(e.KeyCode == Keys.Left && e.Control)
			{
				Backward.PerformClick();
                e.Handled = true;
            }
			else if (e.KeyCode == Keys.Right && e.Control)
			{
				Forward.PerformClick();
                e.Handled = true;
            }
			else if (e.KeyCode == Keys.Space)
			{
				Play.PerformClick();
                e.Handled = true;
                Textbox.Tag = "Space";
            }
			else if (e.KeyCode == Keys.Delete && !Textbox.Focused)
			{
				Remove.PerformClick();
                e.Handled = true;
            }
        }
        private void Textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
			if(Textbox.Tag.ToString() == "Space")
			{
				Textbox.Tag = "";
				e.Handled= true;
			}
        }

        private void Remove_Click(object sender, EventArgs e)
        {
			int current = SentenceList.SelectedIndex;
			if (current == -1) return;
			SentenceList.Items.RemoveAt(current);
			labels.RemoveAt(current);
        }

        private void SentenceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int current = SentenceList.SelectedIndex;
            if (current == -1) return;
            labelformat thislabel = labels[current];
			Textbox.Text = thislabel.text;
			inpoint = thislabel.inpoint;
            InPointLabel.Text = (inpoint / 1e7).ToString();
            outpoint = thislabel.outpoint;
            OutPointLabel.Text = (outpoint / 1e7).ToString();
        }

        private void Save_Click(object sender, EventArgs e)
        {
			try
			{
				
				FileInfo audioinput = new FileInfo(path);
				FileStream file = new FileStream(audioinput.DirectoryName + "/labels.xml", FileMode.OpenOrCreate);
				serializer.Serialize(file, labels);
				file.Close();
			}
			catch { 
			}
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
			source = null;
			clock = null;
			session.Close();
			session.Shutdown();
			session = null;
			MFShutdown();
        }
    }
}
