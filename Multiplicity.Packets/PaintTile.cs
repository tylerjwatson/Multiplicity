using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The PaintTile (0x3F) packet.
    /// </summary>
    public class PaintTile : TerrariaPacket
    {

        public short X { get; set; }

        public short Y { get; set; }

        public byte Color { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaintTile"/> class.
        /// </summary>
        public PaintTile()
            : base((byte)PacketTypes.PaintTile)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaintTile"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public PaintTile(BinaryReader br)
            : base(br)
        {
            this.X = br.ReadInt16();
            this.Y = br.ReadInt16();
            this.Color = br.ReadByte();
        }

        public override string ToString()
        {
            return $"[PaintTile: X = {X} Y = {Y} Color = {Color}]";
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
                br.Write(Color);
            }
        }

        #endregion

    }
}
