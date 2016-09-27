using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The Disconnect (0x2) packet.
    /// </summary>
    public class Disconnect : TerrariaPacket
    {

        public string Reason { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Disconnect"/> class.
        /// </summary>
        public Disconnect()
            : base((byte)PacketTypes.Disconnect)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Multiplicity.Packets.Disconnect"/> class.
        /// </summary>
        /// <param name="reason">Reason for disconnecting the client.</param>
        public Disconnect(string reason)
            : base((byte)PacketTypes.Disconnect)
        {
            this.Reason = reason;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Disconnect"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public Disconnect(BinaryReader br)
            : base(br)
        {
            this.Reason = br.ReadString();
        }

        public override string ToString()
        {
            return $"[Disconnect: Reason = {Reason}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(1 + Reason.Length);
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
                br.Write(Reason);
            }
        }

        #endregion

    }
}
