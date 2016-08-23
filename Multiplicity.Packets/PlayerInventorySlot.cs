using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The PlayerInventorySlot (0x5) packet.
    /// </summary>
    public class PlayerInventorySlot : TerrariaPacket
    {

        public byte PlayerID { get; set; }

        /// <summary>
        /// Gets or sets the SlotID - 0 - 58 = Inventory, 59 - 78 = Armor, 79 - 88 = Dye, 89 - 93 MiscEquips, 94 - 98 = MiscDyes, 99 - 138 = Piggy bank, 139-178 = Safe, 179 = Trash|
        /// </summary>
        public byte SlotID { get; set; }

        public short Stack { get; set; }

        public byte Prefix { get; set; }

        public short ItemNetID { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerInventorySlot"/> class.
        /// </summary>
        public PlayerInventorySlot()
            : base((byte)PacketTypes.PlayerInventorySlot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerInventorySlot"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public PlayerInventorySlot(BinaryReader br)
            : base(br)
        {
            this.PlayerID = br.ReadByte();
            this.SlotID = br.ReadByte();
            this.Stack = br.ReadInt16();
            this.Prefix = br.ReadByte();
            this.ItemNetID = br.ReadInt16();
        }

        public override string ToString()
        {
            return
	            $"[PlayerInventorySlot: PlayerID = {PlayerID} SlotID = {SlotID} Stack = {Stack} Prefix = {Prefix} ItemNetID = {ItemNetID}]";
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
                br.Write(PlayerID);
                br.Write(SlotID);
                br.Write(Stack);
                br.Write(Prefix);
                br.Write(ItemNetID);
            }
        }

        #endregion

    }
}
