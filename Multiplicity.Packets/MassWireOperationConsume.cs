using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The MassWireOperationConsume (0x6E) packet.
    /// </summary>
    public class MassWireOperationConsume : TerrariaPacket
    {

        public short ItemType { get; set; }

        public short Quantity { get; set; }

        public byte PlayerID { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MassWireOperationConsume"/> class.
        /// </summary>
        public MassWireOperationConsume()
            : base((byte)PacketTypes.MassWireOperationConsume)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MassWireOperationConsume"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public MassWireOperationConsume(BinaryReader br)
            : base(br)
        {
            this.ItemType = br.ReadInt16();
            this.Quantity = br.ReadInt16();
            this.PlayerID = br.ReadByte();
        }

        public override string ToString()
        {
            return $"[MassWireOperationConsume: ItemType = {ItemType} Quantity = {Quantity} PlayerID = {PlayerID}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(5);
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
                br.Write(ItemType);
                br.Write(Quantity);
                br.Write(PlayerID);
            }
        }

        #endregion

    }
}
