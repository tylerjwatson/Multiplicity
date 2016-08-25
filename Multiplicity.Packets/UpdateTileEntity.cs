using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The UpdateTileEntity (0x56) packet.
    /// </summary>
    public class UpdateTileEntity : TerrariaPacket
    {

        public int EntityID { get; set; }

        public bool Remove { get; set; }

        public byte EntityType { get; set; }

        public int Id { get; set; }

        public short X { get; set; }

        public short Y { get; set; }

        public short Npc { get; set; }

        public short ItemType { get; set; }

        public byte Prefix { get; set; }

        public short Stack { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateTileEntity"/> class.
        /// </summary>
        public UpdateTileEntity() 
            : base((byte)PacketTypes.UpdateTileEntity)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateTileEntity"/> class.
        /// </summary>
        /// <param name="br"></param>
        public UpdateTileEntity(BinaryReader br) : base(br)
        {
            this.EntityID = br.ReadInt32();
            this.Remove = br.ReadBoolean();
            this.EntityType = br.ReadByte();
            this.Id = br.ReadInt32();
            this.X = br.ReadInt16();
            this.Y = br.ReadInt16();
            this.Npc = br.ReadInt16();
            this.ItemType = br.ReadInt16();
            this.Prefix = br.ReadByte();
            this.Stack = br.ReadInt16();
        }

        public override string ToString()
        {
            return $"[UpdateTileEntity: EntityID = {EntityID} Remove = {Remove} EntityType = {EntityType} Id = {Id} X = {X} Y = {Y} NPC = {Npc} ItemType = {ItemType} Prefix = {Prefix} Stack = {Stack}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(21);
        }

        public override void ToStream(Stream stream, bool includeHeader = true)
        {
            /*
             * Length and ID headers get written in the base packet class.
             */
            if (includeHeader)
            {
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
                br.Write(EntityID);
                br.Write(Remove);
                br.Write(EntityType);
                br.Write(Id);
                br.Write(X);
                br.Write(Y);
                br.Write(Npc);
                br.Write(ItemType);
                br.Write(Prefix);
                br.Write(Stack);
            }
        }

        #endregion

    }
}
