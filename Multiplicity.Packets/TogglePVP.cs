using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The TogglePVP (0x1E) packet.
    /// </summary>
    public class TogglePVP : TerrariaPacket
    {

        public byte PlayerID { get; set; }

        public bool PVPEnabled { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TogglePVP"/> class.
        /// </summary>
        public TogglePVP()
            : base((byte)PacketTypes.TogglePVP)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TogglePVP"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public TogglePVP(BinaryReader br)
            : base(br)
        {
            this.PlayerID = br.ReadByte();
            this.PVPEnabled = br.ReadBoolean();
        }

        public override string ToString()
        {
            return $"[TogglePVP: PlayerID = {PlayerID} PVPEnabled = {PVPEnabled}]";
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
                br.Write(PVPEnabled);
            }
        }

        #endregion

    }
}
