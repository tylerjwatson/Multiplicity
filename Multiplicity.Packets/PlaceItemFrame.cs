using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The PlaceItemFrame (0x59) packet.
    /// </summary>
    public class PlaceItemFrame : TerrariaPacket
    {

        public short X { get; set; }

        public short Y { get; set; }

        public short ItemId { get; set; }

        public byte Prefix { get; set; }

        public short Stack { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaceItemFrame"/> class.
        /// </summary>
        public PlaceItemFrame()
            : base((byte)PacketTypes.PlaceItemFrame)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaceItemFrame"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public PlaceItemFrame(BinaryReader br)
            : base(br)
        {
            this.X = br.ReadInt16();
            this.Y = br.ReadInt16();
            this.ItemId = br.ReadInt16();
            this.Prefix = br.ReadByte();
            this.Stack = br.ReadInt16();
        }

        public override string ToString()
        {
            return $"[PlaceItemFrame: X = {X} Y = {Y} ItemId = {ItemId} Prefix = {Prefix} Stack = {Stack}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(9);
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
                br.Write(ItemId);
                br.Write(Prefix);
                br.Write(Stack);
            }
        }

        #endregion

    }
}
