using System.IO;

namespace Multiplicity.Packets
{
	/// <summary>
	/// The MinionAttackTargetUpdate (73) packet.
	/// </summary>
	public class MinionAttackTargetUpdate : TerrariaPacket
	{
		public byte PlayerId { get; set; }

		public short MinionAttackTarget { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="MinionAttackTargetUpdate"/> class.
		/// </summary>
		public MinionAttackTargetUpdate()
			: base((byte)PacketTypes.MinionAttackTargetUpdate)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MinionAttackTargetUpdate"/> class.
		/// </summary>
		/// <param name="br">br</param>
		public MinionAttackTargetUpdate(BinaryReader br)
			: base(br)
		{
			PlayerId = br.ReadByte();
			MinionAttackTarget = br.ReadInt16();
		}

		public override string ToString()
		{
			return $"[MinionAttackTargetUpdate: PlayerId = {PlayerId} MinionAttackTarget = {MinionAttackTarget}]";
		}

		#region implemented abstract members of TerrariaPacket

		public override short GetLength()
		{
			return (short)(3);
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
				br.Write(PlayerId);
				br.Write(MinionAttackTarget);
			}
		}

		#endregion
	}
}
