using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The ManaEffect (0x2B) packet.
    /// </summary>
    public class ManaEffect : TerrariaPacket
    {

        public byte PlayerID { get; set; }

        public short ManaAmount { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManaEffect"/> class.
        /// </summary>
        public ManaEffect()
            : base((byte)PacketTypes.ManaEffect)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManaEffect"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public ManaEffect(BinaryReader br)
            : base(br)
        {
            this.PlayerID = br.ReadByte();
            this.ManaAmount = br.ReadInt16();
        }

        public override string ToString()
        {
            return $"[ManaEffect: PlayerID = {PlayerID} ManaAmount = {ManaAmount}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(3);
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
                br.Write(ManaAmount);
            }
        }

        #endregion

    }
}
