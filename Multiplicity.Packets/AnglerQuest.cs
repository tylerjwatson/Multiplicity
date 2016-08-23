using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The AnglerQuest (0x4A) packet.
    /// </summary>
    public class AnglerQuest : TerrariaPacket
    {

        public byte Quest { get; set; }

        public bool Completed { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnglerQuest"/> class.
        /// </summary>
        public AnglerQuest()
            : base((byte)PacketTypes.AnglerQuest)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnglerQuest"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public AnglerQuest(BinaryReader br)
            : base(br)
        {
            this.Quest = br.ReadByte();
            this.Completed = br.ReadBoolean();
        }

        public override string ToString()
        {
            return $"[AnglerQuest: Quest = {Quest} Completed = {Completed}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(2);
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
                br.Write(Quest);
                br.Write(Completed);
            }
        }

        #endregion

    }
}
