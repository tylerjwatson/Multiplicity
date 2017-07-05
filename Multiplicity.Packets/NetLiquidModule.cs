using Multiplicity.Packets.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace Multiplicity.Packets
{
	/// <summary>
	/// NetModule communicating bulk updates to liquids in tiles.
	/// </summary>
	public class NetLiquidModule : TerrariaNetModule
	{
		/// <summary>
		/// List of updates made in this module.
		/// </summary>
		public List<LiquidUpdate> Updates { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="NetLiquidModule"/> class.
		/// </summary>
		/// <param name="br"></param>
		public NetLiquidModule(BinaryReader br) : base(br)
		{
			int length = br.ReadUInt16();
			for (int i = 0; i < length; i++)
			{
				Updates.Add(new LiquidUpdate(br));
			}
		}

		public override short GetLength()
		{
			return (short)(2 + (Updates.Count * 4));
		}

		public override void ToStream(Stream stream, bool includeHeader = true)
		{
			if (includeHeader)
			{
				base.ToStream(stream, includeHeader);
			}

			using (BinaryWriter bw = new BinaryWriter(stream, new System.Text.UTF8Encoding(), leaveOpen: true))
			{
				bw.Write((ushort)Updates.Count);
				foreach (var update in Updates)
				{
					update.ToStream(stream);
				}
			}
		}
	}
}