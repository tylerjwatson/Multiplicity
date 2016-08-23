using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The Unlock (0x34) packet.
    /// </summary>
    public class Unlock : TerrariaPacket
    {

        /// <summary>
        /// Gets or sets the Type - Values: 1 = Chest Unlock, 2 = Door Unlock|
        /// </summary>
        public byte Type { get; set; }

        public short X { get; set; }

        public short Y { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Unlock"/> class.
        /// </summary>
        public Unlock()
            : base((byte)PacketTypes.Unlock)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Unlock"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public Unlock(BinaryReader br)
            : base(br)
        {
            this.Type = br.ReadByte();
            this.X = br.ReadInt16();
            this.Y = br.ReadInt16();
        }

        public override string ToString()
        {
            return $"[Unlock: Type = {Type} X = {X} Y = {Y}]";
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
                br.Write(Type);
                br.Write(X);
                br.Write(Y);
            }
        }

        #endregion

    }
}
