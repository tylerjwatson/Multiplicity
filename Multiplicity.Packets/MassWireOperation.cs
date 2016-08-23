using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The MassWireOperation (0x6D) packet.
    /// </summary>
    public class MassWireOperation : TerrariaPacket
    {

        public short StartX { get; set; }

        public short StartY { get; set; }

        public short EndX { get; set; }

        public short EndY { get; set; }

        /// <summary>
        /// Gets or sets the ToolMode - BitFlags: 1 = Red, 2 = Green, 4 = Blue, 8 = Yellow, 16 = Actuator, 32 = Cutter|
        /// </summary>
        public byte ToolMode { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MassWireOperation"/> class.
        /// </summary>
        public MassWireOperation()
            : base((byte)PacketTypes.MassWireOperation)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MassWireOperation"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public MassWireOperation(BinaryReader br)
            : base(br)
        {
            this.StartX = br.ReadInt16();
            this.StartY = br.ReadInt16();
            this.EndX = br.ReadInt16();
            this.EndY = br.ReadInt16();
            this.ToolMode = br.ReadByte();
        }

        public override string ToString()
        {
            return
	            $"[MassWireOperation: StartX = {StartX} StartY = {StartY} EndX = {EndX} EndY = {EndY} ToolMode = {ToolMode}]";
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
                br.Write(StartX);
                br.Write(StartY);
                br.Write(EndX);
                br.Write(EndY);
                br.Write(ToolMode);
            }
        }

        #endregion

    }
}
