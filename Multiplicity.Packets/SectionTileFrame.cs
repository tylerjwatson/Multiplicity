using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The SectionTileFrame (0xB) packet.
    /// </summary>
    public class SectionTileFrame : TerrariaPacket
    {

        public short StartX { get; set; }

        public short StartY { get; set; }

        public short EndX { get; set; }

        public short EndY { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SectionTileFrame"/> class.
        /// </summary>
        public SectionTileFrame()
            : base((byte)PacketTypes.SectionTileFrame)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SectionTileFrame"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public SectionTileFrame(BinaryReader br)
            : base(br)
        {
            this.StartX = br.ReadInt16();
            this.StartY = br.ReadInt16();
            this.EndX = br.ReadInt16();
            this.EndY = br.ReadInt16();
        }

        public override string ToString()
        {
            return $"[SectionTileFrame: StartX = {StartX} StartY = {StartY} EndX = {EndX} EndY = {EndY}]";
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
                br.Write(StartX);
                br.Write(StartY);
                br.Write(EndX);
                br.Write(EndY);
            }
        }

        #endregion

    }
}
