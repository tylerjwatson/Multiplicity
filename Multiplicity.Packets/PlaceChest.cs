using System.IO;

namespace Multiplicity.Packets
{
	/// <summary>
	/// The PlaceChest (34) packet.
	/// </summary>
	public class PlaceChest : TerrariaPacket
	{
		public byte Action { get; set; }
		public short X { get; set; }
		public short Y { get; set; }
		public short Style { get; set; }
		public short ChestID { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="PlaceChest"/> class.
		/// </summary>
		public PlaceChest() : base((byte)PacketTypes.PlaceChest) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="PlaceChest"/> class.
		/// </summary>
		/// <param name="br">br</param>
		public PlaceChest(BinaryReader br)
			: base(br)
		{
			Action = br.ReadByte();
			X = br.ReadInt16();
			Y = br.ReadInt16();
			Style = br.ReadInt16();

			//ID = br.ReadInt16();
			ChestID = 0; //TODO: Client detection? This is particular field is server->client only
		}

		public override string ToString()
		{
			return $"[{nameof(PlaceChest)}: Action={Action},X={X},Y={Y},Style={Style}]";
		}

		#region implemented abstract members of TerrariaPacket

		public override short GetLength()
		{
			return (short)(7);
		}

		public override void ToStream(Stream stream, bool includeHeader = true)
		{
			/*
             * Length and ID headers get written in the base packet class.
             */
			if (includeHeader)
			{
				base.ToStream(stream, includeHeader);
			}

			/*
             * Always make sure to not close the stream when serializing.
             * 
             * It is up to the caller to decide if the underlying stream
             * gets closed.  If this is a network stream we do not want
             * the regressions of unconditionally closing the TCP socket
             * once the payload of data has been sent to the client.
             */
			using (BinaryWriter writer = new BinaryWriter(stream, new System.Text.UTF8Encoding(), leaveOpen: true))
			{
				writer.Write(Action);
				writer.Write(X);
				writer.Write(Y);
				writer.Write(Style);
				writer.Write(ChestID);
			}
		}

		#endregion
	}
}