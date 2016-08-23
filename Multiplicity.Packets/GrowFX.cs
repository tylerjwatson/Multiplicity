using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The GrowFX (0x70) packet.
    /// </summary>
    public class GrowFX : TerrariaPacket
    {

        public bool GrowEffect { get; set; }

        public short X { get; set; }

        public short Y { get; set; }

        public byte Height { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GrowFX"/> class.
        /// </summary>
        public GrowFX()
            : base((byte)PacketTypes.GrowFX)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GrowFX"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public GrowFX(BinaryReader br)
            : base(br)
        {
            this.GrowEffect = br.ReadBoolean();
            this.X = br.ReadInt16();
            this.Y = br.ReadInt16();
            this.Height = br.ReadByte();
        }

        public override string ToString()
        {
            return $"[GrowFX: GrowEffect = {GrowEffect} X = {X} Y = {Y} Height = {Height}]";
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
                br.Write(GrowEffect);
                br.Write(X);
                br.Write(Y);
                br.Write(Height);
            }
        }

        #endregion

    }
}
