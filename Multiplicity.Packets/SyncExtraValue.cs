using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The SyncExtraValue (0x5C) packet.
    /// </summary>
    public class SyncExtraValue : TerrariaPacket
    {

        public short NPCIndex { get; set; }

        public float ExtraValue { get; set; }

        public float X { get; set; }

        public float Y { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncExtraValue"/> class.
        /// </summary>
        public SyncExtraValue()
            : base((byte)PacketTypes.SyncExtraValue)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncExtraValue"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public SyncExtraValue(BinaryReader br)
            : base(br)
        {
            this.NPCIndex = br.ReadInt16();
            this.ExtraValue = br.ReadSingle();
            this.X = br.ReadSingle();
            this.Y = br.ReadSingle();
        }

        public override string ToString()
        {
            return $"[SyncExtraValue: NPCIndex = {NPCIndex} ExtraValue = {ExtraValue} X = {X} Y = {Y}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(14);
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
                br.Write(NPCIndex);
                br.Write(ExtraValue);
                br.Write(X);
                br.Write(Y);
            }
        }

        #endregion

    }
}
