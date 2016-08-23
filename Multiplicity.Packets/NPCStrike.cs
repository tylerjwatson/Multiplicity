using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The NPCStrike (0x1C) packet.
    /// </summary>
    public class NPCStrike : TerrariaPacket
    {

        public short NPCID { get; set; }

        public short Damage { get; set; }

        public float Knockback { get; set; }

        public byte Direction { get; set; }

        public bool Crit { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NPCStrike"/> class.
        /// </summary>
        public NPCStrike()
            : base((byte)PacketTypes.NPCStrike)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NPCStrike"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public NPCStrike(BinaryReader br)
            : base(br)
        {
            this.NPCID = br.ReadInt16();
            this.Damage = br.ReadInt16();
            this.Knockback = br.ReadSingle();
            this.Direction = br.ReadByte();
            this.Crit = br.ReadBoolean();
        }

        public override string ToString()
        {
            return
	            $"[NPCStrike: NPCID = {NPCID} Damage = {Damage} Knockback = {Knockback} Direction = {Direction} Crit = {Crit}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(10);
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
                br.Write(NPCID);
                br.Write(Damage);
                br.Write(Knockback);
                br.Write(Direction);
                br.Write(Crit);
            }
        }

        #endregion

    }
}
