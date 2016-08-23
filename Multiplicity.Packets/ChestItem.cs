using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The ChestItem (0x20) packet.
    /// </summary>
    public class ChestItem : TerrariaPacket
    {

        public short ChestID { get; set; }

        public byte ItemSlot { get; set; }

        public short Stack { get; set; }

        public byte Prefix { get; set; }

        public short ItemNetID { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChestItem"/> class.
        /// </summary>
        public ChestItem()
            : base((byte)PacketTypes.ChestItem)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChestItem"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public ChestItem(BinaryReader br)
            : base(br)
        {
            this.ChestID = br.ReadInt16();
            this.ItemSlot = br.ReadByte();
            this.Stack = br.ReadInt16();
            this.Prefix = br.ReadByte();
            this.ItemNetID = br.ReadInt16();
        }

        public override string ToString()
        {
            return
	            $"[ChestItem: ChestID = {ChestID} ItemSlot = {ItemSlot} Stack = {Stack} Prefix = {Prefix} ItemNetID = {ItemNetID}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(8);
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
                br.Write(ChestID);
                br.Write(ItemSlot);
                br.Write(Stack);
                br.Write(Prefix);
                br.Write(ItemNetID);
            }
        }

        #endregion

    }
}
