using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The UpdateItemOwner (0x16) packet.
    /// </summary>
    public class UpdateItemOwner : TerrariaPacket
    {

        public short ItemID { get; set; }

        public byte PlayerID { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateItemOwner"/> class.
        /// </summary>
        public UpdateItemOwner()
            : base((byte)PacketTypes.UpdateItemOwner)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateItemOwner"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public UpdateItemOwner(BinaryReader br)
            : base(br)
        {
            this.ItemID = br.ReadInt16();
            this.PlayerID = br.ReadByte();
        }

        public override string ToString()
        {
            return $"[UpdateItemOwner: ItemID = {ItemID} PlayerID = {PlayerID}]";
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
                br.Write(ItemID);
                br.Write(PlayerID);
            }
        }

        #endregion

    }
}
