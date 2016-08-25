using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The ReleaseNPC (0x47) packet.
    /// </summary>
    public class ReleaseNPC : TerrariaPacket
    {

        public int X { get; set; }

        public int Y { get; set; }

        public short Type { get; set; }

        public byte Style { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReleaseNPC"/> class.
        /// </summary>
        public ReleaseNPC() 
            : base((byte)PacketTypes.ReleaseNPC)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReleaseNPC"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public ReleaseNPC(BinaryReader br) 
            : base(br)
        {
            this.X = br.ReadInt32();
            this.Y = br.ReadInt32();
            this.Type = br.ReadInt16();
            this.Style = br.ReadByte();
        }

        public override string ToString()
        {
            return $"[ReleaseNPC: X = {X} Y = {Y} Type = {Type} Style = {Style}]";
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
                br.Write(X);
                br.Write(Y);
                br.Write(Type);
                br.Write(Style);
            }
        }

        #endregion

    }
}
