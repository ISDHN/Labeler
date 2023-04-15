namespace Labeler
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SentenceList = new System.Windows.Forms.ListBox();
            this.Backward = new System.Windows.Forms.Button();
            this.Play = new System.Windows.Forms.Button();
            this.Forward = new System.Windows.Forms.Button();
            this.Add = new System.Windows.Forms.Button();
            this.Remove = new System.Windows.Forms.Button();
            this.Save = new System.Windows.Forms.Button();
            this.Textbox = new System.Windows.Forms.TextBox();
            this.SetIn = new System.Windows.Forms.Button();
            this.ToIn = new System.Windows.Forms.Button();
            this.SetOut = new System.Windows.Forms.Button();
            this.ToOut = new System.Windows.Forms.Button();
            this.InPointLabel = new System.Windows.Forms.Label();
            this.OutPointLabel = new System.Windows.Forms.Label();
            this.Progress = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.Progress)).BeginInit();
            this.SuspendLayout();
            // 
            // SentenceList
            // 
            this.SentenceList.AllowDrop = true;
            this.SentenceList.FormattingEnabled = true;
            this.SentenceList.HorizontalScrollbar = true;
            this.SentenceList.ItemHeight = 12;
            this.SentenceList.Location = new System.Drawing.Point(375, 10);
            this.SentenceList.Name = "SentenceList";
            this.SentenceList.Size = new System.Drawing.Size(138, 196);
            this.SentenceList.TabIndex = 0;
            this.SentenceList.SelectedIndexChanged += new System.EventHandler(this.SentenceList_SelectedIndexChanged);
            this.SentenceList.DragDrop += new System.Windows.Forms.DragEventHandler(this.SentenceList_DragDrop);
            this.SentenceList.DragEnter += new System.Windows.Forms.DragEventHandler(this.SentenceList_DragEnter);
            // 
            // Backward
            // 
            this.Backward.Enabled = false;
            this.Backward.Location = new System.Drawing.Point(11, 179);
            this.Backward.Name = "Backward";
            this.Backward.Size = new System.Drawing.Size(30, 30);
            this.Backward.TabIndex = 1;
            this.Backward.Text = "<-";
            this.Backward.UseVisualStyleBackColor = true;
            this.Backward.Click += new System.EventHandler(this.Backward_Click);
            // 
            // Play
            // 
            this.Play.Enabled = false;
            this.Play.Location = new System.Drawing.Point(47, 179);
            this.Play.Name = "Play";
            this.Play.Size = new System.Drawing.Size(30, 30);
            this.Play.TabIndex = 2;
            this.Play.Text = "▶";
            this.Play.UseVisualStyleBackColor = true;
            this.Play.Click += new System.EventHandler(this.Play_Click);
            // 
            // Forward
            // 
            this.Forward.Enabled = false;
            this.Forward.Location = new System.Drawing.Point(83, 179);
            this.Forward.Name = "Forward";
            this.Forward.Size = new System.Drawing.Size(30, 30);
            this.Forward.TabIndex = 3;
            this.Forward.Text = "->";
            this.Forward.UseVisualStyleBackColor = true;
            this.Forward.Click += new System.EventHandler(this.Forward_Click);
            // 
            // Add
            // 
            this.Add.Font = new System.Drawing.Font("SimSun", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Add.ForeColor = System.Drawing.Color.LimeGreen;
            this.Add.Location = new System.Drawing.Point(145, 180);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(30, 30);
            this.Add.TabIndex = 4;
            this.Add.Text = "+";
            this.Add.UseVisualStyleBackColor = true;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // Remove
            // 
            this.Remove.Font = new System.Drawing.Font("SimSun", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Remove.ForeColor = System.Drawing.Color.Red;
            this.Remove.Location = new System.Drawing.Point(181, 179);
            this.Remove.Name = "Remove";
            this.Remove.Size = new System.Drawing.Size(30, 30);
            this.Remove.TabIndex = 5;
            this.Remove.Text = "-";
            this.Remove.UseVisualStyleBackColor = true;
            this.Remove.Click += new System.EventHandler(this.Remove_Click);
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(277, 183);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(75, 23);
            this.Save.TabIndex = 6;
            this.Save.Text = "保存";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // Textbox
            // 
            this.Textbox.Location = new System.Drawing.Point(12, 131);
            this.Textbox.Name = "Textbox";
            this.Textbox.Size = new System.Drawing.Size(340, 21);
            this.Textbox.TabIndex = 7;
            this.Textbox.Tag = "";
            this.Textbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Textbox_KeyPress);
            // 
            // SetIn
            // 
            this.SetIn.Location = new System.Drawing.Point(12, 43);
            this.SetIn.Name = "SetIn";
            this.SetIn.Size = new System.Drawing.Size(75, 23);
            this.SetIn.TabIndex = 8;
            this.SetIn.Text = "标记入点";
            this.SetIn.UseVisualStyleBackColor = true;
            this.SetIn.Click += new System.EventHandler(this.SetIn_Click);
            // 
            // ToIn
            // 
            this.ToIn.Enabled = false;
            this.ToIn.Location = new System.Drawing.Point(12, 83);
            this.ToIn.Name = "ToIn";
            this.ToIn.Size = new System.Drawing.Size(75, 23);
            this.ToIn.TabIndex = 9;
            this.ToIn.Text = "转到入点";
            this.ToIn.UseVisualStyleBackColor = true;
            this.ToIn.Click += new System.EventHandler(this.ToIn_Click);
            // 
            // SetOut
            // 
            this.SetOut.Location = new System.Drawing.Point(197, 43);
            this.SetOut.Name = "SetOut";
            this.SetOut.Size = new System.Drawing.Size(75, 23);
            this.SetOut.TabIndex = 10;
            this.SetOut.Text = "标记出点";
            this.SetOut.UseVisualStyleBackColor = true;
            this.SetOut.Click += new System.EventHandler(this.SetOut_Click);
            // 
            // ToOut
            // 
            this.ToOut.Enabled = false;
            this.ToOut.Location = new System.Drawing.Point(197, 83);
            this.ToOut.Name = "ToOut";
            this.ToOut.Size = new System.Drawing.Size(75, 23);
            this.ToOut.TabIndex = 11;
            this.ToOut.Text = "转到出点";
            this.ToOut.UseVisualStyleBackColor = true;
            this.ToOut.Click += new System.EventHandler(this.ToOut_Click);
            // 
            // InPointLabel
            // 
            this.InPointLabel.AutoSize = true;
            this.InPointLabel.Location = new System.Drawing.Point(93, 74);
            this.InPointLabel.Name = "InPointLabel";
            this.InPointLabel.Size = new System.Drawing.Size(11, 12);
            this.InPointLabel.TabIndex = 12;
            this.InPointLabel.Text = "0";
            // 
            // OutPointLabel
            // 
            this.OutPointLabel.AutoSize = true;
            this.OutPointLabel.Location = new System.Drawing.Point(287, 74);
            this.OutPointLabel.Name = "OutPointLabel";
            this.OutPointLabel.Size = new System.Drawing.Size(11, 12);
            this.OutPointLabel.TabIndex = 13;
            this.OutPointLabel.Text = "0";
            // 
            // Progress
            // 
            this.Progress.Enabled = false;
            this.Progress.Location = new System.Drawing.Point(12, 11);
            this.Progress.Maximum = 0;
            this.Progress.Name = "Progress";
            this.Progress.Size = new System.Drawing.Size(357, 45);
            this.Progress.TabIndex = 14;
            this.Progress.TickStyle = System.Windows.Forms.TickStyle.None;
            this.Progress.Scroll += new System.EventHandler(this.Progress_Scroll);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(534, 231);
            this.Controls.Add(this.OutPointLabel);
            this.Controls.Add(this.InPointLabel);
            this.Controls.Add(this.ToOut);
            this.Controls.Add(this.SetOut);
            this.Controls.Add(this.ToIn);
            this.Controls.Add(this.SetIn);
            this.Controls.Add(this.Textbox);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.Remove);
            this.Controls.Add(this.Add);
            this.Controls.Add(this.Forward);
            this.Controls.Add(this.Play);
            this.Controls.Add(this.Backward);
            this.Controls.Add(this.SentenceList);
            this.Controls.Add(this.Progress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Labaler";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.Progress)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox SentenceList;
        private System.Windows.Forms.Button Backward;
        private System.Windows.Forms.Button Play;
        private System.Windows.Forms.Button Forward;
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.Button Remove;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.TextBox Textbox;
        private System.Windows.Forms.Button SetIn;
        private System.Windows.Forms.Button ToIn;
        private System.Windows.Forms.Button SetOut;
        private System.Windows.Forms.Button ToOut;
        private System.Windows.Forms.Label InPointLabel;
        private System.Windows.Forms.Label OutPointLabel;
        private System.Windows.Forms.TrackBar Progress;
    }
}

