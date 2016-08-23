using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The ClientUUID (0x44) packet.
    /// </summary>
    public class ClientUUID : TerrariaPacket
    {

        /// <summary>
        /// Gets or sets the UUID - |
        /// </summary>
        public string UUID { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientUUID"/> class.
        /// </summary>
        public ClientUUID()
            : base((byte)PacketTypes.ClientUUID)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientUUID"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public ClientUUID(BinaryReader br)
            : base(br)
        {
            this.UUID = br.ReadString();
        }

        public override string ToString()
        {
            return $"[ClientUUID: {UUID}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(1 + UUID.Length);
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
                br.Write(UUID);
            }
        }

        #endregion

    }
}
