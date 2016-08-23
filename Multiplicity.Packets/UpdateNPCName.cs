using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The UpdateNPCName (0x38) packet.
    /// </summary>
    public class UpdateNPCName : TerrariaPacket
    {

        public short NPCID { get; set; }

        /// <summary>
        /// Gets or sets the Name - Read-only from the client|
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateNPCName"/> class.
        /// </summary>
        public UpdateNPCName()
            : base((byte)PacketTypes.UpdateNPCName)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateNPCName"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public UpdateNPCName(BinaryReader br)
            : base(br)
        {
            this.NPCID = br.ReadInt16();
            this.Name = br.ReadString();
        }

        public override string ToString()
        {
            return $"[UpdateNPCName: NPCID = {NPCID} Name = {Name}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(3 + Name.Length);
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
                br.Write(NPCID);
                br.Write(Name);
            }
        }

        #endregion

    }
}
