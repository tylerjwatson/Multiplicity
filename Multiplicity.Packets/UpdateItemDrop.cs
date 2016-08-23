using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The UpdateItemDrop (0x15) packet.
    /// </summary>
    public class UpdateItemDrop : TerrariaPacket
    {

        /// <summary>
        /// Gets or sets the ItemID - If below 400 and NetID 0 Then Set NullIf ItemID is 400 Then New Item|
        /// </summary>
        public short ItemID { get; set; }

        public float PositionX { get; set; }

        public float PositionY { get; set; }

        public float VelocityX { get; set; }

        public float VelocityY { get; set; }

        public short StackSize { get; set; }

        public byte Prefix { get; set; }

        /// <summary>
        /// Gets or sets the NoDelay - If 0 then ownIgnore = 0 and ownTime = 100|
        /// </summary>
        public byte NoDelay { get; set; }

        public short ItemNetID { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateItemDrop"/> class.
        /// </summary>
        public UpdateItemDrop()
            : base((byte)PacketTypes.UpdateItemDrop)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateItemDrop"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public UpdateItemDrop(BinaryReader br)
            : base(br)
        {
            this.ItemID = br.ReadInt16();
            this.PositionX = br.ReadSingle();
            this.PositionY = br.ReadSingle();
            this.VelocityX = br.ReadSingle();
            this.VelocityY = br.ReadSingle();
            this.StackSize = br.ReadInt16();
            this.Prefix = br.ReadByte();
            this.NoDelay = br.ReadByte();
            this.ItemNetID = br.ReadInt16();
        }

        public override string ToString()
        {
            return
	            $"[UpdateItemDrop: ItemID = {ItemID} PositionX = {PositionX} PositionY = {PositionY} VelocityX = {VelocityX} VelocityY = {VelocityY} StackSize = {StackSize} Prefix = {Prefix} NoDelay = {NoDelay} ItemNetID = {ItemNetID}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(24);
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
                br.Write(PositionX);
                br.Write(PositionY);
                br.Write(VelocityX);
                br.Write(VelocityY);
                br.Write(StackSize);
                br.Write(Prefix);
                br.Write(NoDelay);
                br.Write(ItemNetID);
            }
        }

        #endregion

    }
}
