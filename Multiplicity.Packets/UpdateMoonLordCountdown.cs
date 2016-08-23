using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The UpdateMoonLordCountdown (0x67) packet.
    /// </summary>
    public class UpdateMoonLordCountdown : TerrariaPacket
    {

        public int MoonLordCountdown { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateMoonLordCountdown"/> class.
        /// </summary>
        public UpdateMoonLordCountdown()
            : base((byte)PacketTypes.UpdateMoonLordCountdown)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateMoonLordCountdown"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public UpdateMoonLordCountdown(BinaryReader br)
            : base(br)
        {
            this.MoonLordCountdown = br.ReadInt32();
        }

        public override string ToString()
        {
            return $"[UpdateMoonLordCountdown: MoonLordCountdown = {MoonLordCountdown}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(4);
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
                br.Write(MoonLordCountdown);
            }
        }

        #endregion

    }
}
