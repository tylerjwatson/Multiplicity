using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Multiplicity.Packets
{
	/// <summary>
	/// The CrystalInvasionStart () packet.
	/// </summary>
	public class CrystalInvasionStart : TerrariaPacket
	{
		public short X { get; set; }

		public short Y { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="CrystalInvasionStart"/> class.
		/// </summary>
		public CrystalInvasionStart()
			: base((byte)PacketTypes.CrystalInvasionStart)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CrystalInvasionStart"/> class.
		/// </summary>
		/// <param name="br">br</param>
		public CrystalInvasionStart(BinaryReader br)
			:base(br)
		{
			X = br.ReadInt16();
			Y = br.ReadInt16();
		}

		public override string ToString()
		{
			return $"[CrystalInvasionStart: X = {X} Y = {Y}]";
		}

		#region implemented abstract members of TerrariaPacket

		public override short GetLength()
		{
			return (short)(4);
		}

		public override void ToStream(Stream stream, bool includeHeader = true)
		{
			/*
             * Length and ID headers get written in the base packet class.
             */
			if (includeHeader) {
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
			using (BinaryWriter br = new BinaryWriter(stream, new System.Text.UTF8Encoding(), leaveOpen: true)) {
				br.Write(X);
				br.Write(Y);
			}
		}

		#endregion
	}
}
