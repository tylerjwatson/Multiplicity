using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The StrikeNPCwithHeldItem (0x18) packet.
    /// </summary>
    public class StrikeNPCwithHeldItem : TerrariaPacket
    {

        public short NPCID { get; set; }

        public byte PlayerID { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StrikeNPCwithHeldItem"/> class.
        /// </summary>
        public StrikeNPCwithHeldItem()
            : base((byte)PacketTypes.StrikeNPCwithHeldItem)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StrikeNPCwithHeldItem"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public StrikeNPCwithHeldItem(BinaryReader br)
            : base(br)
        {
            this.NPCID = br.ReadInt16();
            this.PlayerID = br.ReadByte();
        }

        public override string ToString()
        {
            return $"[StrikeNPCwithHeldItem: NPCID = {NPCID} PlayerID = {PlayerID}]";
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
                br.Write(PlayerID);
            }
        }

        #endregion

    }
}
