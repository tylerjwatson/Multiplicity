using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The NPCHomeUpdate (0x3C) packet.
    /// </summary>
    public class NPCHomeUpdate : TerrariaPacket
    {

        public short NPCID { get; set; }

        public short HomeTileX { get; set; }

        public short HomeTileY { get; set; }

        public byte Homeless { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NPCHomeUpdate"/> class.
        /// </summary>
        public NPCHomeUpdate()
            : base((byte)PacketTypes.NPCHomeUpdate)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NPCHomeUpdate"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public NPCHomeUpdate(BinaryReader br)
            : base(br)
        {
            this.NPCID = br.ReadInt16();
            this.HomeTileX = br.ReadInt16();
            this.HomeTileY = br.ReadInt16();
            this.Homeless = br.ReadByte();
        }

        public override string ToString()
        {
            return
	            $"[NPCHomeUpdate: NPCID = {NPCID} HomeTileX = {HomeTileX} HomeTileY = {HomeTileY} Homeless = {Homeless}]";
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
                br.Write(HomeTileX);
                br.Write(HomeTileY);
                br.Write(Homeless);
            }
        }

        #endregion

    }
}
