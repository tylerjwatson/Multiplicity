using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The PlaceTileEntity (0x57) packet.
    /// </summary>
    public class PlaceTileEntity : TerrariaPacket
    {

        public short X { get; set; }

        public short Y { get; set; }

        /// <summary>
        /// Gets or sets the Type - 2 = Logic Sensor 1 = Item Frame 0 = Training Dummy|
        /// </summary>
        public byte Type { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaceTileEntity"/> class.
        /// </summary>
        public PlaceTileEntity()
            : base((byte)PacketTypes.PlaceTileEntity)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaceTileEntity"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public PlaceTileEntity(BinaryReader br)
            : base(br)
        {
            this.X = br.ReadInt16();
            this.Y = br.ReadInt16();
            this.Type = br.ReadByte();
        }

        public override string ToString()
        {
            return $"[PlaceTileEntity: X = {X} Y = {Y} Type = {Type}]";
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
                br.Write(X);
                br.Write(Y);
                br.Write(Type);
            }
        }

        #endregion

    }
}
