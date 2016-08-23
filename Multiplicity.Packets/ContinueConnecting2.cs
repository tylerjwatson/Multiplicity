using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The ContinueConnecting2 (0x6) packet.
    /// </summary>
    public class ContinueConnecting2 : TerrariaPacket
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ContinueConnecting2"/> class.
        /// </summary>
        public ContinueConnecting2()
            : base((byte)PacketTypes.ContinueConnecting2)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContinueConnecting2"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public ContinueConnecting2(BinaryReader br)
            : base(br)
        {
        }

        public override string ToString()
        {
            return string.Format("[ContinueConnecting2]");
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(0);
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
            }
        }

        #endregion

    }
}
