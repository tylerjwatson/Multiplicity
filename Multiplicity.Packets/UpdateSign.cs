using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The UpdateSign (0x2F) packet.
    /// </summary>
    public class UpdateSign : TerrariaPacket
    {

        public short SignID { get; set; }

        public short X { get; set; }

        public short Y { get; set; }

        public string Text { get; set; }

        public byte PlayerID { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSign"/> class.
        /// </summary>
        public UpdateSign()
            : base((byte)PacketTypes.UpdateSign)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSign"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public UpdateSign(BinaryReader br)
            : base(br)
        {
            this.SignID = br.ReadInt16();
            this.X = br.ReadInt16();
            this.Y = br.ReadInt16();
            this.Text = br.ReadString();
            this.PlayerID = br.ReadByte();
        }

        public override string ToString()
        {
            return $"[UpdateSign: SignID = {SignID} X = {X} Y = {Y} Text = {Text} PlayerID = {PlayerID}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(8 + Text.Length);
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
                br.Write(SignID);
                br.Write(X);
                br.Write(Y);
                br.Write(Text);
                br.Write(PlayerID);
            }
        }

        #endregion

    }
}
