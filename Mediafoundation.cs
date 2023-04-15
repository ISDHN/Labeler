using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Labeler.Interop{
	public enum MFCLOCK_STATE{
		MFCLOCK_STATE_INVALID = 0,
		MFCLOCK_STATE_RUNNING = (MFCLOCK_STATE_INVALID + 1),
		MFCLOCK_STATE_STOPPED = (MFCLOCK_STATE_RUNNING + 1),
		MFCLOCK_STATE_PAUSED = (MFCLOCK_STATE_STOPPED + 1)
	}
	public class Interop{
		public static Guid MF_TOPOLOGY_RESOLUTION_STATUS = new Guid(0x494bbcde, 0xb031, 0x4e38, 0x97, 0xc4, 0xd5, 0x42, 0x2d, 0xd6, 0x18, 0xdc);
		public static Guid MF_TOPONODE_SOURCE = Guid.Parse("835c58ec-e075-4bc7-bcba-4de000df9ae6");
		public static Guid MF_TOPONODE_PRESENTATION_DESCRIPTOR = Guid.Parse("835c58ed-e075-4bc7-bcba-4de000df9ae6");
		public static Guid MF_TOPONODE_STREAM_DESCRIPTOR = Guid.Parse("835c58ee-e075-4bc7-bcba-4de000df9ae6");
		public static Guid MFMediaType_Audio = Guid.Parse("73647561-0000-0010-8000-00AA00389B71");
		public static Guid MFMediaType_Video = Guid.Parse("73646976-0000-0010-8000-00AA00389B71");
		public static Guid MR_VIDEO_RENDER_SERVICE = new Guid(0x1092a86c, 0xab1a, 0x459a, 0xa3, 0x36, 0x83, 0x1f, 0xbc, 0x4d, 0x11, 0xff);
		public static Guid MF_RATE_CONTROL_SERVICE = Guid.Parse("866fa297-b802-4bf8-9dc9-5e3b6a9f53c9");
		public static Guid MR_AUDIO_POLICY_SERVICE = new Guid(0x911fd737, 0x6775, 0x4ab0, 0xa6, 0x14, 0x29, 0x78, 0x62, 0xfd, 0xac, 0x88);
		public static Guid MF_PD_DURATION = new Guid(0x6c990d33, 0xbb8e, 0x477a, 0x85, 0x98, 0xd, 0x5d, 0x96, 0xfc, 0xd8, 0x8a);
		public static Guid MMDeviceEnumerator = Guid.Parse("BCDE0395-E52F-467C-8E3D-C4579291692E");
		public const uint MF_SDK_VERSION = 0x0001;
		public const uint MF_API_VERSION = 0x0070;
		public const uint MF_VERSION = (MF_SDK_VERSION << 16) | MF_API_VERSION;
		public const uint MFSTARTUP_NOSOCKET = 0x1;
		public const uint MFSTARTUP_FULL = 0x0;
		public const uint MF_RESOLUTION_MEDIASOURCE = 0x00000001;
		public const uint MF_RESOLUTION_BYTESTREAM = 0x00000002;
		public const uint MF_RESOLUTION_CONTENT_DOES_NOT_HAVE_TO_MATCH_EXTENSION_OR_MIME_TYPE = 0x00000010;
		public const uint MF_TOPOLOGY_OUTPUT_NODE = 0x0;
		public const uint MF_TOPOLOGY_SOURCESTREAM_NODE = 0x1;
        public const uint MESessionEnded = 107;
		public const uint MESessionStarted = 103;
		public const uint MESessionTopologyStatus = 111;
		public const uint MESessionTopologySet = 101;
		public const uint MESessionStopped = 105;
		[DllImport("mfplat.dll", SetLastError = true, PreserveSig = true)]
		public static extern int MFStartup(uint Version, uint flags);
		[DllImport("mfplat.dll")]
		public static extern int MFShutdown();
		[DllImport("mf.dll", SetLastError = true, PreserveSig = true)]
		public static extern int MFCreateMediaSession(IntPtr pConfiguration, out IMFMediaSession ppMediaSession);
		[DllImport("mf.dll", SetLastError = true, PreserveSig = true)]
		public static extern int MFCreateSourceResolver(out IMFSourceResolver ppISourceResolver);
		[DllImport("mf.dll", SetLastError = true, PreserveSig = true)]
		public static extern int MFCreateTopology(out IMFTopology ppTopo);
		[DllImport("mf.dll", SetLastError = true, PreserveSig = true)]
		public static extern int MFCreateTopologyNode(uint NodeType, out IMFTopologyNode ppNode);
		[DllImport("mf.dll", SetLastError = true, PreserveSig = true)]
		public static extern int MFCreateAudioRendererActivate(out IMFActivate ppActivate);
		[DllImport("mf.dll", SetLastError = true, PreserveSig = true)]
		public static extern int MFGetService(IUnknown punkObject, ref Guid guidService, ref Guid riid, out IUnknown ppvObject);
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct MFCLOCK_PROPERTIES{
		public ulong qwCorrelationRate;
		public Guid guidClockId;
		public uint dwClockFlags;
		public ulong qwClockFrequency;
		public uint dwClockTolerance;
		public uint dwClockJitter;
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct PropVariant{
		public ushort vt;
		public ushort wReserved1;
		public ushort wReserved2;
		public ushort wReserved3;
		public ulong unionmember;
	}
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("00000000-0000-0000-C000-000000000046")]
	public interface IUnknown{
		int QueryInterface(ref Guid iid, out IUnknown ppvObj);
		int AddRef();
		int Release();
	}
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("90377834-21D0-4dee-8214-BA2E3E6C1127")]
	public interface IMFMediaSession{
		//IMFMediaEventGenerator
		[PreserveSig]
		int GetEvent(uint dwFlags, out IMFMediaEvent ppEvent);
		int BeginGetEvent(IUnknown pCallback, IUnknown punkState);
		int EndGetEvent(IUnknown pResult, out IMFMediaEvent ppEvent);
		int QueueEvent(uint met, ref Guid guidExtendedType, int hrStatus, ref PropVariant pvValue);
		//IMFMediaSession
		int SetTopology(uint dwSetTopologyFlags, IMFTopology pTopology);
		int ClearTopologies();
		int Start(ref Guid pguidTimeFormat, ref PropVariant pvarStartPosition);
		int Pause();
		int Stop();
		int Close();
		int Shutdown();
		int GetClock(out IMFClock ppClock);
		int GetSessionCapabilities(out uint pdwCaps);
		int GetFullTopology(uint dwGetFullTopologyFlags, ulong TopoId, IMFTopology ppFullTopology);
	}

	[ComVisible(true), ComImport, Guid("FBE5A32D-A497-4b61-BB85-97B1A848A6E3")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IMFSourceResolver{
		int CreateObjectFromURL(string pwszURL, uint dwFlags, IUnknown pProps, out uint pObjectType, out IUnknown ppObject);
	}

	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("83CF873A-F6DA-4bc8-823F-BACFD55DC433")]
	public interface IMFTopology{
		//IMFAttributes
		int GetItem(ref Guid guidKey, [In, Out] ref PropVariant pValue);
		int GetItemType(ref Guid guidKey, out uint pType);
		int CompareItem(ref Guid guidKey, ref PropVariant Value, out bool pbResult);
		int Compare(IMFAttributes pTheirs, uint MatchType, out bool pbResult);
		int GetUINT32(ref Guid guidKey, out uint punValue);
		int GetUINT64(ref Guid guidKey, out ulong punValue);
		int GetDouble(ref Guid guidKey, out double pfValue);
		int GetGUID(ref Guid guidKey, out Guid pguidValue);
		int GetStringLength(ref Guid guidKey, out uint pcchLength);
		int GetString(ref Guid guidKey, StringBuilder pwszValue, uint cchBufSize, [In, Out] ref uint pcchLength);
		int GetAllocatedString(ref Guid guidKey, StringBuilder ppwszValue, out uint pcchLength);
		int GetBlobSize(ref Guid guidKey, out uint pcbBlobSize);
		int GetBlob(ref Guid guidKey, out ushort pBuf, uint cbBufSize, [In, Out] ref uint pcbBlobSize);
		int GetAllocatedBlob(ref Guid guidKey, out ushort ppBuf, out uint pcbSize);
		int GetUnknown(ref Guid guidKey, Guid riid, out IUnknown ppv);
		int SetItem(ref Guid guidKey, ref PropVariant Value);
		int DeleteItem(ref Guid guidKey);
		int DeleteAllItems();
		int SetUINT32(ref Guid guidKey, uint unValue);
		int SetUINT64(ref Guid guidKey, ulong unValue);
		int SetDouble(ref Guid guidKey, double fValue);
		int SetGUID(ref Guid guidKey, ref Guid guidValue);
		int SetString(ref Guid guidKey, string wszValue);
		int SetBlob(ref Guid guidKey, ref ushort pBuf, uint cbBufSize);
		int SetUnknown(ref Guid guidKey, IUnknown pUnknown);
		int LockStore();
		int UnlockStore();
		int GetCount(out uint pcItems);
		int GetItemByIndex(uint unIndex, out Guid pguidKey, [In, Out] ref PropVariant pValue);
		int CopyAllItems(IMFAttributes pDest);
		//IMFTopology
		int GetTopologyID(out ulong pID);
		int AddNode(IMFTopologyNode pNode);
		int RemoveNode(IMFTopologyNode pNode);
		int GetNodeCount(out ushort pwNodes);
		int GetNode(ushort wIndex, out IMFTopologyNode ppNode);
		int Clear();
		int CloneFrom(IMFTopology pTopology);
		int GetNodeByID(ulong qwTopoNodeID, out IMFTopologyNode ppNode);
		int GetSourceNodeCollection(out IUnknown ppCollection);
		int GetOutputNodeCollection(out IUnknown ppCollection);
	}

	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("279a808d-aec7-40c8-9c6b-a6b492c78a66")]
	public interface IMFMediaSource{
		//IMFMediaEventGenerator
		int GetEvent(uint dwFlags, out IMFMediaEvent ppEvent);
		int BeginGetEvent(IUnknown pCallback, IUnknown punkState);
		int EndGetEvent(IUnknown pResult, out IMFMediaEvent ppEvent);
		int QueueEvent(uint met, ref Guid guidExtendedType, int hrStatus, ref PropVariant pvValue);
		//IMFMediaSource
		int GetCharacteristics(out uint pdwCharacteristics);
		int CreatePresentationDescriptor(out IMFPresentationDescriptor ppPresentationDescriptor);
		int Start(IMFPresentationDescriptor pPresentationDescriptor, IUnknown pguidTimeFormat, ref PropVariant pvarStartPosition);
		int Stop();
		int Pause();
		int Shutdown();
	}

	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("03cb2711-24d7-4db6-a17f-f3a7a479a536")]
	public interface IMFPresentationDescriptor{
		//IMFAttributes
		int GetItem(ref Guid guidKey, [In, Out] ref PropVariant pValue);
		int GetItemType(ref Guid guidKey, out uint pType);
		int CompareItem(ref Guid guidKey, ref PropVariant Value, out bool pbResult);
		int Compare(IMFAttributes pTheirs, uint MatchType, out bool pbResult);
		int GetUINT32(ref Guid guidKey, out uint punValue);
		int GetUINT64(ref Guid guidKey, out ulong punValue);
		int GetDouble(ref Guid guidKey, out double pfValue);
		int GetGUID(ref Guid guidKey, out Guid pguidValue);
		int GetStringLength(ref Guid guidKey, out uint pcchLength);
		int GetString(ref Guid guidKey, StringBuilder pwszValue, uint cchBufSize, [In, Out] ref uint pcchLength);
		int GetAllocatedString(ref Guid guidKey, StringBuilder ppwszValue, out uint pcchLength);
		int GetBlobSize(ref Guid guidKey, out uint pcbBlobSize);
		int GetBlob(ref Guid guidKey, out ushort pBuf, uint cbBufSize, [In, Out] ref uint pcbBlobSize);
		int GetAllocatedBlob(ref Guid guidKey, out ushort ppBuf, out uint pcbSize);
		int GetUnknown(ref Guid guidKey, Guid riid, out IUnknown ppv);
		int SetItem(ref Guid guidKey, ref PropVariant Value);
		int DeleteItem(ref Guid guidKey);
		int DeleteAllItems();
		int SetUINT32(ref Guid guidKey, uint unValue);
		int SetUINT64(ref Guid guidKey, ulong unValue);
		int SetDouble(ref Guid guidKey, double fValue);
		int SetGUID(ref Guid guidKey, ref Guid guidValue);
		int SetString(ref Guid guidKey, string wszValue);
		int SetBlob(ref Guid guidKey, ref ushort pBuf, uint cbBufSize);
		int SetUnknown(ref Guid guidKey, IUnknown pUnknown);
		int LockStore();
		int UnlockStore();
		int GetCount(out uint pcItems);
		int GetItemByIndex(uint unIndex, out Guid pguidKey, [In, Out] ref PropVariant pValue);
		int CopyAllItems(IMFAttributes pDest);
		//IMFPresentationDescriptor
		int GetStreamDescriptorCount(out uint pdwDescriptorCount);
		int GetStreamDescriptorByIndex(uint dwIndex, out bool pfSelected, out IMFStreamDescriptor ppDescriptor);
		int SelectStream(uint dwDescriptorIndex);
		int DeselectStream(uint dwDescriptorIndex);
		int Clone(out IMFPresentationDescriptor ppPresentationDescriptor);
	}



	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("2cd2d921-c447-44a7-a13c-4adabfc247e3")]
	public interface IMFAttributes{
		int GetItem(ref Guid guidKey, [In, Out] ref PropVariant pValue);
		int GetItemType(ref Guid guidKey, out uint pType);
		int CompareItem(ref Guid guidKey, ref PropVariant Value, out bool pbResult);
		int Compare(IMFAttributes pTheirs, uint MatchType, out bool pbResult);
		int GetUINT32(ref Guid guidKey, out uint punValue);
		int GetUINT64(ref Guid guidKey, out ulong punValue);
		int GetDouble(ref Guid guidKey, out double pfValue);
		int GetGUID(ref Guid guidKey, out Guid pguidValue);
		int GetStringLength(ref Guid guidKey, out uint pcchLength);
		int GetString(ref Guid guidKey, StringBuilder pwszValue, uint cchBufSize, [In, Out] ref uint pcchLength);
		int GetAllocatedString(ref Guid guidKey, StringBuilder ppwszValue, out uint pcchLength);
		int GetBlobSize(ref Guid guidKey, out uint pcbBlobSize);
		int GetBlob(ref Guid guidKey, out ushort pBuf, uint cbBufSize, [In, Out] ref uint pcbBlobSize);
		int GetAllocatedBlob(ref Guid guidKey, out ushort ppBuf, out uint pcbSize);
		int GetUnknown(ref Guid guidKey, Guid riid, out IUnknown ppv);
		int SetItem(ref Guid guidKey, ref PropVariant Value);
		int DeleteItem(ref Guid guidKey);
		int DeleteAllItems();
		int SetUINT32(ref Guid guidKey, uint unValue);
		int SetUINT64(ref Guid guidKey, ulong unValue);
		int SetDouble(ref Guid guidKey, double fValue);
		int SetGUID(ref Guid guidKey, ref Guid guidValue);
		int SetString(ref Guid guidKey, string wszValue);
		int SetBlob(ref Guid guidKey, ref ushort pBuf, uint cbBufSize);
		int SetUnknown(ref Guid guidKey, IUnknown pUnknown);
		int LockStore();
		int UnlockStore();
		int GetCount(out uint pcItems);
		int GetItemByIndex(uint unIndex, out Guid pguidKey, [In, Out] ref PropVariant pValue);
		int CopyAllItems(IMFAttributes pDest);
	}

	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("56c03d9c-9dbb-45f5-ab4b-d80f47c05938")]
	public interface IMFStreamDescriptor{
		//IMFAttributes
		int GetItem(ref Guid guidKey, [In, Out] ref PropVariant pValue);
		int GetItemType(ref Guid guidKey, out uint pType);
		int CompareItem(ref Guid guidKey, ref PropVariant Value, out bool pbResult);
		int Compare(IMFAttributes pTheirs, uint MatchType, out bool pbResult);
		int GetUINT32(ref Guid guidKey, out uint punValue);
		int GetUINT64(ref Guid guidKey, out ulong punValue);
		int GetDouble(ref Guid guidKey, out double pfValue);
		int GetGUID(ref Guid guidKey, out Guid pguidValue);
		int GetStringLength(ref Guid guidKey, out uint pcchLength);
		int GetString(ref Guid guidKey, StringBuilder pwszValue, uint cchBufSize, [In, Out] ref uint pcchLength);
		int GetAllocatedString(ref Guid guidKey, StringBuilder ppwszValue, out uint pcchLength);
		int GetBlobSize(ref Guid guidKey, out uint pcbBlobSize);
		int GetBlob(ref Guid guidKey, out ushort pBuf, uint cbBufSize, [In, Out] ref uint pcbBlobSize);
		int GetAllocatedBlob(ref Guid guidKey, out ushort ppBuf, out uint pcbSize);
		int GetUnknown(ref Guid guidKey, Guid riid, out IUnknown ppv);
		int SetItem(ref Guid guidKey, ref PropVariant Value);
		int DeleteItem(ref Guid guidKey);
		int DeleteAllItems();
		int SetUINT32(ref Guid guidKey, uint unValue);
		int SetUINT64(ref Guid guidKey, ulong unValue);
		int SetDouble(ref Guid guidKey, double fValue);
		int SetGUID(ref Guid guidKey, ref Guid guidValue);
		int SetString(ref Guid guidKey, string wszValue);
		int SetBlob(ref Guid guidKey, ref ushort pBuf, uint cbBufSize);
		int SetUnknown(ref Guid guidKey, IUnknown pUnknown);
		int LockStore();
		int UnlockStore();
		int GetCount(out uint pcItems);
		int GetItemByIndex(uint unIndex, out Guid pguidKey, [In, Out] ref PropVariant pValue);
		int CopyAllItems(IMFAttributes pDest);
		//IMFStreamDescriptor
		int GetStreamIdentifier(out uint pdwStreamIdentifier);
		int GetMediaTypeHandler(out IMFMediaTypeHandler ppMediaTypeHandler);
	}

	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("83CF873A-F6DA-4bc8-823F-BACFD55DC430")]
	public interface IMFTopologyNode{
		//IMFAttributes
		int GetItem(ref Guid guidKey, [In, Out] ref PropVariant pValue);
		int GetItemType(ref Guid guidKey, out uint pType);
		int CompareItem(ref Guid guidKey, ref PropVariant Value, out bool pbResult);
		int Compare(IMFAttributes pTheirs, uint MatchType, out bool pbResult);
		int GetUINT32(ref Guid guidKey, out uint punValue);
		int GetUINT64(ref Guid guidKey, out ulong punValue);
		int GetDouble(ref Guid guidKey, out double pfValue);
		int GetGUID(ref Guid guidKey, out Guid pguidValue);
		int GetStringLength(ref Guid guidKey, out uint pcchLength);
		int GetString(ref Guid guidKey, StringBuilder pwszValue, uint cchBufSize, [In, Out] ref uint pcchLength);
		int GetAllocatedString(ref Guid guidKey, StringBuilder ppwszValue, out uint pcchLength);
		int GetBlobSize(ref Guid guidKey, out uint pcbBlobSize);
		int GetBlob(ref Guid guidKey, out ushort pBuf, uint cbBufSize, [In, Out] ref uint pcbBlobSize);
		int GetAllocatedBlob(ref Guid guidKey, out ushort ppBuf, out uint pcbSize);
		int GetUnknown(ref Guid guidKey, Guid riid, out IUnknown ppv);
		int SetItem(ref Guid guidKey, ref PropVariant Value);
		int DeleteItem(ref Guid guidKey);
		int DeleteAllItems();
		int SetUINT32(ref Guid guidKey, uint unValue);
		int SetUINT64(ref Guid guidKey, ulong unValue);
		int SetDouble(ref Guid guidKey, double fValue);
		int SetGUID(ref Guid guidKey, ref Guid guidValue);
		int SetString(ref Guid guidKey, string wszValue);
		int SetBlob(ref Guid guidKey, ref ushort pBuf, uint cbBufSize);
		int SetUnknown(ref Guid guidKey, IUnknown pUnknown);
		int LockStore();
		int UnlockStore();
		int GetCount(out uint pcItems);
		int GetItemByIndex(uint unIndex, out Guid pguidKey, [In, Out] ref PropVariant pValue);
		int CopyAllItems(IMFAttributes pDest);
		//IMFTopologyNode
		int SetObject(IUnknown pObject);
		int GetObject(out IUnknown ppObject);
		int GetNodeType(out uint pType);
		int GetTopoNodeID(out ulong pID);
		int SetTopoNodeID(ulong ullTopoID);
		int GetInputCount(out uint pcInputs);
		int GetOutputCount(out uint pcOutputs);
		int ConnectOutput(uint dwOutputIndex, IMFTopologyNode pDownstreamNode, uint dwInputIndexOnDownstreamNode);
		int DisconnectOutput(uint dwOutputIndex);
		int GetInput(uint dwInputIndex, out IMFTopologyNode ppUpstreamNode, out uint pdwOutputIndexOnUpstreamNode);
		int GetOutput(uint dwOutputIndex, out IMFTopologyNode ppDownstreamNode, out uint pdwInputIndexOnDownstreamNode);
		int SetOutputPrefType(uint dwOutputIndex, IUnknown pType);
		int GetOutputPrefType(uint dwOutputIndex, out IUnknown ppType);
		int SetInputPrefType(uint dwInputIndex, IUnknown pType);
		int GetInputPrefType(uint dwInputIndex, out IUnknown ppType);
		int CloneFrom(IMFTopologyNode pNode);
	}

	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("7FEE9E9A-4A89-47a6-899C-B6A53A70FB67")]
	public interface IMFActivate{
		//IMFAttributes
		int GetItem(ref Guid guidKey, [In, Out] ref PropVariant pValue);
		int GetItemType(ref Guid guidKey, out uint pType);
		int CompareItem(ref Guid guidKey, ref PropVariant Value, out bool pbResult);
		int Compare(IMFAttributes pTheirs, uint MatchType, out bool pbResult);
		int GetUINT32(ref Guid guidKey, out uint punValue);
		int GetUINT64(ref Guid guidKey, out ulong punValue);
		int GetDouble(ref Guid guidKey, out double pfValue);
		int GetGUID(ref Guid guidKey, out Guid pguidValue);
		int GetStringLength(ref Guid guidKey, out uint pcchLength);
		int GetString(ref Guid guidKey, StringBuilder pwszValue, uint cchBufSize, [In, Out] ref uint pcchLength);
		int GetAllocatedString(ref Guid guidKey, StringBuilder ppwszValue, out uint pcchLength);
		int GetBlobSize(ref Guid guidKey, out uint pcbBlobSize);
		int GetBlob(ref Guid guidKey, out ushort pBuf, uint cbBufSize, [In, Out] ref uint pcbBlobSize);
		int GetAllocatedBlob(ref Guid guidKey, out ushort ppBuf, out uint pcbSize);
		int GetUnknown(ref Guid guidKey, Guid riid, out IUnknown ppv);
		int SetItem(ref Guid guidKey, ref PropVariant Value);
		int DeleteItem(ref Guid guidKey);
		int DeleteAllItems();
		int SetUINT32(ref Guid guidKey, uint unValue);
		int SetUINT64(ref Guid guidKey, ulong unValue);
		int SetDouble(ref Guid guidKey, double fValue);
		int SetGUID(ref Guid guidKey, ref Guid guidValue);
		int SetString(ref Guid guidKey, string wszValue);
		int SetBlob(ref Guid guidKey, ref ushort pBuf, uint cbBufSize);
		int SetUnknown(ref Guid guidKey, IUnknown pUnknown);
		int LockStore();
		int UnlockStore();
		int GetCount(out uint pcItems);
		int GetItemByIndex(uint unIndex, out Guid pguidKey, [In, Out] ref PropVariant pValue);
		int CopyAllItems(IMFAttributes pDest);
		//IMFActivate
		int ActivateObject(ref Guid riid, out IUnknown ppv);
		int ShutdownObject();
		int DetachObject();
	}

	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("e93dcf6c-4b07-4e1e-8123-aa16ed6eadf5")]
	public interface IMFMediaTypeHandler{
		int IsMediaTypeSupported(IUnknown pMediaType, out IUnknown ppMediaType);
		int GetMediaTypeCount(out uint pdwTypeCount);
		int GetMediaTypeByIndex(uint dwIndex, out IUnknown ppType);

		int SetCurrentMediaType(IUnknown pMediaType);

		int GetCurrentMediaType(out IUnknown ppMediaType);

		int GetMajorType(out Guid pguidMajorType);
	}

	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("DF598932-F10C-4E39-BBA2-C308F101DAA3")]
	public interface IMFMediaEvent{
		//IMFAttributes
		int GetItem(ref Guid guidKey, [In, Out] ref PropVariant pValue);
		int GetItemType(ref Guid guidKey, out uint pType);
		int CompareItem(ref Guid guidKey, ref PropVariant Value, out bool pbResult);
		int Compare(IMFAttributes pTheirs, uint MatchType, out bool pbResult);
		int GetUINT32(ref Guid guidKey, out uint punValue);
		int GetUINT64(ref Guid guidKey, out ulong punValue);
		int GetDouble(ref Guid guidKey, out double pfValue);
		int GetGUID(ref Guid guidKey, out Guid pguidValue);
		int GetStringLength(ref Guid guidKey, out uint pcchLength);
		int GetString(ref Guid guidKey, StringBuilder pwszValue, uint cchBufSize, [In, Out] ref uint pcchLength);
		int GetAllocatedString(ref Guid guidKey, StringBuilder ppwszValue, out uint pcchLength);
		int GetBlobSize(ref Guid guidKey, out uint pcbBlobSize);
		int GetBlob(ref Guid guidKey, out ushort pBuf, uint cbBufSize, [In, Out] ref uint pcbBlobSize);
		int GetAllocatedBlob(ref Guid guidKey, out ushort ppBuf, out uint pcbSize);
		int GetUnknown(ref Guid guidKey, Guid riid, out IUnknown ppv);
		int SetItem(ref Guid guidKey, ref PropVariant Value);
		int DeleteItem(ref Guid guidKey);
		int DeleteAllItems();
		int SetUINT32(ref Guid guidKey, uint unValue);
		int SetUINT64(ref Guid guidKey, ulong unValue);
		int SetDouble(ref Guid guidKey, double fValue);
		int SetGUID(ref Guid guidKey, ref Guid guidValue);
		int SetString(ref Guid guidKey, string wszValue);
		int SetBlob(ref Guid guidKey, ref ushort pBuf, uint cbBufSize);
		int SetUnknown(ref Guid guidKey, IUnknown pUnknown);
		int LockStore();
		int UnlockStore();
		int GetCount(out uint pcItems);
		int GetItemByIndex(uint unIndex, out Guid pguidKey, [In, Out] ref PropVariant pValue);
		int CopyAllItems(IMFAttributes pDest);
		//IMFMediaEvent
		int GetType(out uint pmet);
		int GetExtendedType(out Guid pguidExtendedType);
		int GetStatus(out int phrStatus);
		int GetValue(out PropVariant pvValue);
	}
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("2eb1e945-18b8-4139-9b1a-d5d584818530")]
	public interface IMFClock{
		int GetClockCharacteristics(out uint pdwCharacteristics);		
		int GetCorrelatedTime(uint dwReserved,out ulong pllClockTime,out ulong phnsSystemTime);		
		int GetContinuityKey(out uint pdwContinuityKey);		
		int GetState(uint dwReserved,out MFCLOCK_STATE peClockState);
		int GetProperties(out MFCLOCK_PROPERTIES pClockProperties);
		
	};
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true), ComImport, Guid("868CE85C-8EA9-4f55-AB82-B009A910A805")]
	public interface IMFPresentationClock{
		//IMFClock
		int GetClockCharacteristics(out uint pdwCharacteristics);
		int GetCorrelatedTime(uint dwReserved, out ulong pllClockTime, out ulong phnsSystemTime);
		int GetContinuityKey(out uint pdwContinuityKey);
		int GetState(uint dwReserved, out MFCLOCK_STATE peClockState);
		int GetProperties(out MFCLOCK_PROPERTIES pClockProperties);
		//IMFPresentationClock
		int SetTimeSource(IUnknown pTimeSource);
		int GetTimeSource(out IUnknown ppTimeSource);
		int GetTime(out ulong phnsClockTime);
		int AddClockStateSink(IUnknown pStateSink);
		int RemoveClockStateSink(IUnknown pStateSink);
		int Start(ulong llClockStartOffset);
		int Stop();
		int Pause();
		
	};
}