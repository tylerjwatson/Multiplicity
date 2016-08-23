using System.IO;

namespace Multiplicity.Packets.Models
{
	public class Tile
	{
		public byte Flags { get; set; }
		
		public byte Flags2 { get; set; }
		
		public byte Flags3 { get; set; }

		public Tile(BinaryReader br)
		{
			

		}

	}
}
