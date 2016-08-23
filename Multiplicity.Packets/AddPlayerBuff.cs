using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The AddPlayerBuff (0x37) packet.
    /// </summary>
    public class AddPlayerBuff : TerrariaPacket
    {

        public byte PlayerID { get; set; }

        public byte Buff { get; set; }

        public short Time { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddPlayerBuff"/> class.
        /// </summary>
        public AddPlayerBuff()
            : base((byte)PacketTypes.AddPlayerBuff)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddPlayerBuff"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public AddPlayerBuff(BinaryReader br)
            : base(br)
        {
            this.PlayerID = br.ReadByte();
            this.Buff = br.ReadByte();
            this.Time = br.ReadInt16();
        }

        public override string ToString()
        {
            return $"[AddPlayerBuff: PlayerID = {PlayerID} Buff = {Buff} Time = {Time}]";
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
                br.Write(PlayerID);
                br.Write(Buff);
                br.Write(Time);
            }
        }

        #endregion

    }
}
