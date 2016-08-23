using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The SendPassword (0x26) packet.
    /// </summary>
    public class SendPassword : TerrariaPacket
    {

        public string Password { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SendPassword"/> class.
        /// </summary>
        public SendPassword()
            : base((byte)PacketTypes.SendPassword)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SendPassword"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public SendPassword(BinaryReader br)
            : base(br)
        {
            this.Password = br.ReadString();
        }

        public override string ToString()
        {
            return $"[SendPassword: Password = {Password}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(1 + Password.Length);
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
                br.Write(Password);
            }
        }

        #endregion

    }
}
