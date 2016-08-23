using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The PlayerActive (0xE) packet.
    /// </summary>
    public class PlayerActive : TerrariaPacket
    {

        public byte PlayerID { get; set; }

        public bool Active { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerActive"/> class.
        /// </summary>
        public PlayerActive()
            : base((byte)PacketTypes.PlayerActive)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerActive"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public PlayerActive(BinaryReader br)
            : base(br)
        {
            this.PlayerID = br.ReadByte();
            this.Active = br.ReadBoolean();
        }

        public override string ToString()
        {
            return $"[PlayerActive: PlayerID = {PlayerID} Active = {Active}]";
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
                br.Write(PlayerID);
                br.Write(Active);
            }
        }

        #endregion

    }
}
