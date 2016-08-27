using System;
using System.IO;

namespace Multiplicity.Packets
{
	/// <summary>
	/// The SetChestName (33) packet.
	/// </summary>
	public class SetChestName : TerrariaPacket
	{
		public short ChestID { get; set; }
		public short X { get; set; }
		public short Y { get; set; }
		public byte NameLength { get; set; }
		public string Name { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="SetChestName"/> class.
		/// </summary>
		public SetChestName() : base((byte)PacketTypes.SetChestName) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="SetChestName"/> class.
		/// </summary>
		/// <param name="br">br</param>
		public SetChestName(BinaryReader br)
			: base(br)
		{
			ChestID = br.ReadInt16();

			X = br.ReadInt16();
			Y = br.ReadInt16();
			NameLength = br.ReadByte();
			Name = String.Empty;

			if (NameLength != 0)
			{
				if (NameLength <= 20)
					Name = br.ReadString();
				else if (NameLength != 255)
					NameLength = 0;
			}
		}

		public override string ToString()
		{
			return $"[{nameof(SetChestName)}: ChestID={ChestID},X={X},Y={Y},TextLength={NameLength},Text={Name}]";
		}

		#region implemented abstract members of TerrariaPacket

		public override short GetLength()
		{
			const short Length = 7;
			if (Name == null)
				return Length;

			return (short)(Length + Name.Length);
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
				writer.Write(ChestID);
				writer.Write(X);
				writer.Write(Y);
				if (Name != null)
				{
					writer.Write(NameLength);
					writer.Write(Name);
				}
			}
		}

		#endregion
	}
}