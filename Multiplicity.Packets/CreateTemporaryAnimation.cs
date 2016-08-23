using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The CreateTemporaryAnimation (0x4D) packet.
    /// </summary>
    public class CreateTemporaryAnimation : TerrariaPacket
    {

        public short AnimationType { get; set; }

        public ushort TileType { get; set; }

        public short X { get; set; }

        public short Y { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTemporaryAnimation"/> class.
        /// </summary>
        public CreateTemporaryAnimation()
            : base((byte)PacketTypes.CreateTemporaryAnimation)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTemporaryAnimation"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public CreateTemporaryAnimation(BinaryReader br)
            : base(br)
        {
            this.AnimationType = br.ReadInt16();
            this.TileType = br.ReadUInt16();
            this.X = br.ReadInt16();
            this.Y = br.ReadInt16();
        }

        public override string ToString()
        {
            return $"[CreateTemporaryAnimation: AnimationType = {AnimationType} TileType = {TileType} X = {X} Y = {Y}]";
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
                br.Write(AnimationType);
                br.Write(TileType);
                br.Write(X);
                br.Write(Y);
            }
        }

        #endregion

    }
}
