using System.Drawing;
using System.IO;
using Multiplicity.Packets.Extensions;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The ChatMessage (0x19) packet.
    /// </summary>
    public class ChatMessage : TerrariaPacket
    {

        /// <summary>
        /// Gets or sets the PlayerID - If 255 Then No Name|
        /// </summary>
        public byte PlayerID { get; set; }

        /// <summary>
        /// Gets or sets the MessageColor - Client cannot change colors|
        /// </summary>
        public Color MessageColor { get; set; }

        public string Message { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatMessage"/> class.
        /// </summary>
        public ChatMessage()
            : base((byte)PacketTypes.ChatMessage)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatMessage"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public ChatMessage(BinaryReader br)
            : base(br)
        {
            this.PlayerID = br.ReadByte();
            this.MessageColor = br.ReadColor();
            this.Message = br.ReadString();
        }

        public override string ToString()
        {
            return $"[ChatMessage: PlayerID = {PlayerID} MessageColor = {MessageColor} Message = {Message}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(5 + Message.Length);
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
                br.Write(PlayerID);
                br.Write(MessageColor);
                br.Write(Message);
            }
        }

        #endregion

    }
}
