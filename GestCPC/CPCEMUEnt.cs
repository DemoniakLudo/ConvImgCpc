using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public class CPCEMUEnt {
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x30)]
	public string id;			// "MV - CPCEMU Disk-File\r\nDisk-Info\r\n"
	public byte NbTracks;
	public byte NbHeads;
	public short TrackSize;		// 0x1300 = 256 + ( 512 * nbsecteurs )
	public byte[] TrackSizeTable = new byte[204];	// Si "EXTENDED CPC DSK File"
};


