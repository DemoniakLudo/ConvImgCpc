using System.Runtime.InteropServices;

public class CPCEMUTrack {
	public const int MAX_SECTS = 29;	// Nbre maxi de secteurs/pistes

	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x10)]
	public string id;		// "Track-Info\r\n"
	public byte Track;
	public byte Head;
	public short Unused;
	public byte SectSize;	// 2
	public byte NbSect;		// 9
	public byte Gap3;		// 0x4E
	public byte OctRemp;	// 0xE5
	public CPCEMUSect[] Sect = new CPCEMUSect[MAX_SECTS];

	public CPCEMUTrack() {
		for (int i = 0; i < MAX_SECTS; i++)
			Sect[i] = new CPCEMUSect();
	}
}

