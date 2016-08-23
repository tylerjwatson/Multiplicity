using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The SendTileSquare (0x14) packet.
    /// </summary>
    public class SendTileSquare : TerrariaPacket
    {

        public short Size { get; set; }

        public short TileX { get; set; }

        public short TileY { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SendTileSquare"/> class.
        /// </summary>
        public SendTileSquare()
            : base((byte)PacketTypes.SendTileSquare)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SendTileSquare"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public SendTileSquare(BinaryReader br)
            : base(br)
        {
            this.Size = br.ReadInt16();
            this.TileX = br.ReadInt16();
            this.TileY = br.ReadInt16();
        }

        public override string ToString()
        {
            return $"[SendTileSquare: Size = {Size} TileX = {TileX} TileY = {TileY}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(6);
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
                br.Write(Size);
                br.Write(TileX);
                br.Write(TileY);
            }
        }

        #endregion

    }
}
