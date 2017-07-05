using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplicity.Packets.Models
{
	/// <summary>
	/// Represents an update made to a tile's liquid.
	/// </summary>
	public class LiquidUpdate
	{
		/// <summary>
		/// X Coordinate of tile updated.
		/// </summary>
		public int X { get; set; }

		/// <summary>
		/// Y Coordinate of tile updated.
		/// </summary>
		public int Y { get; set; }

		/// <summary>
		/// Liquid amount in tile updated.
		/// </summary>
		public byte Liquid { get; set; }

		/// <summary>
		/// Liquid type in tile updated.
		/// </summary>
		public byte LiquidType { get; set; }

		/// <summary>
		/// Reads from the given reader and initializes a new instance of the <see cref="LiquidUpdate"/> class.
		/// </summary>
		/// <param name="br">Reader to initialize instance from.</param>
		public LiquidUpdate(BinaryReader br)
		{
			int packedCoords = br.ReadInt32();
			X = (packedCoords >> 16) & ushort.MaxValue;
			Y = packedCoords & ushort.MaxValue;
			Liquid = br.ReadByte();
			LiquidType = br.ReadByte();
		}

		/// <summary>
		/// Writes this instance to the given stream.
		/// </summary>
		/// <param name="stream">Stream to write contents to.</param>
		public void ToStream(Stream stream)
		{
			using (BinaryWriter bw = new BinaryWriter(stream, new System.Text.UTF8Encoding(), leaveOpen: true))
			{
				int packedCoords = (X << 16) + Y;
				bw.Write(packedCoords);
				bw.Write(Liquid);
				bw.Write(LiquidType);
			}
		}
	}
}
