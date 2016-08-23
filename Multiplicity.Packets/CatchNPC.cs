using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The CatchNPC (0x46) packet.
    /// </summary>
    public class CatchNPC : TerrariaPacket
    {

        public short NPCID { get; set; }

        public byte Who { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CatchNPC"/> class.
        /// </summary>
        public CatchNPC()
            : base((byte)PacketTypes.CatchNPC)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CatchNPC"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public CatchNPC(BinaryReader br)
            : base(br)
        {
            this.NPCID = br.ReadInt16();
            this.Who = br.ReadByte();
        }

        public override string ToString()
        {
            return $"[CatchNPC: NPCID = {NPCID} Who = {Who}]";
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
                br.Write(NPCID);
                br.Write(Who);
            }
        }

        #endregion

    }
}
