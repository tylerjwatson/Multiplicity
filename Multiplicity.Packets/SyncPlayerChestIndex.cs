using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The SyncPlayerChestIndex (0x50) packet.
    /// </summary>
    public class SyncPlayerChestIndex : TerrariaPacket
    {

        public byte Player { get; set; }

        public short Chest { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncPlayerChestIndex"/> class.
        /// </summary>
        public SyncPlayerChestIndex()
            : base((byte)PacketTypes.SyncPlayerChestIndex)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncPlayerChestIndex"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public SyncPlayerChestIndex(BinaryReader br)
            : base(br)
        {
            this.Player = br.ReadByte();
            this.Chest = br.ReadInt16();
        }

        public override string ToString()
        {
            return $"[SyncPlayerChestIndex: Player = {Player} Chest = {Chest}]";
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
                br.Write(Player);
                br.Write(Chest);
            }
        }

        #endregion

    }
}
