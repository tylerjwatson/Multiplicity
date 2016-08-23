using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The PlaceObject (0x4F) packet.
    /// </summary>
    public class PlaceObject : TerrariaPacket
    {

        public short X { get; set; }

        public short Y { get; set; }

        public short Type { get; set; }

        public short Style { get; set; }

        public byte Alternate { get; set; }

        public sbyte Random { get; set; }

        public bool Direction { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaceObject"/> class.
        /// </summary>
        public PlaceObject()
            : base((byte)PacketTypes.PlaceObject)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaceObject"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public PlaceObject(BinaryReader br)
            : base(br)
        {
            this.X = br.ReadInt16();
            this.Y = br.ReadInt16();
            this.Type = br.ReadInt16();
            this.Style = br.ReadInt16();
            this.Alternate = br.ReadByte();
            this.Random = br.ReadSByte();
            this.Direction = br.ReadBoolean();
        }

        public override string ToString()
        {
            return
	            $"[PlaceObject: X = {X} Y = {Y} Type = {Type} Style = {Style} Alternate = {Alternate} Random = {Random} Direction = {Direction}]";
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
                br.Write(Alternate);
                br.Write(Random);
                br.Write(Direction);
            }
        }

        #endregion

    }
}
