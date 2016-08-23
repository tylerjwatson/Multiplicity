using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The SetNPCShopItem (0x68) packet.
    /// </summary>
    public class SetNPCShopItem : TerrariaPacket
    {

        public byte Slot { get; set; }

        public short ItemType { get; set; }

        public short Stack { get; set; }

        public byte Prefix { get; set; }

        public int Value { get; set; }

        /// <summary>
        /// Gets or sets the Flags - BitFlags: 1 = BuyOnce|
        /// </summary>
        public byte Flags { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SetNPCShopItem"/> class.
        /// </summary>
        public SetNPCShopItem()
            : base((byte)PacketTypes.SetNPCShopItem)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SetNPCShopItem"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public SetNPCShopItem(BinaryReader br)
            : base(br)
        {
            this.Slot = br.ReadByte();
            this.ItemType = br.ReadInt16();
            this.Stack = br.ReadInt16();
            this.Prefix = br.ReadByte();
            this.Value = br.ReadInt32();
            this.Flags = br.ReadByte();
        }

        public override string ToString()
        {
            return
	            $"[SetNPCShopItem: Slot = {Slot} ItemType = {ItemType} Stack = {Stack} Prefix = {Prefix} Value = {Value} Flags = {Flags}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(11);
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
                br.Write(Slot);
                br.Write(ItemType);
                br.Write(Stack);
                br.Write(Prefix);
                br.Write(Value);
                br.Write(Flags);
            }
        }

        #endregion

    }
}
