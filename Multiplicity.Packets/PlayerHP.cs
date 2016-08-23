using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The PlayerHP (0x10) packet.
    /// </summary>
    public class PlayerHP : TerrariaPacket
    {

        public byte PlayerID { get; set; }

        public short HP { get; set; }

        public short MaxHP { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerHP"/> class.
        /// </summary>
        public PlayerHP()
            : base((byte)PacketTypes.PlayerHP)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerHP"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public PlayerHP(BinaryReader br)
            : base(br)
        {
            this.PlayerID = br.ReadByte();
            this.HP = br.ReadInt16();
            this.MaxHP = br.ReadInt16();
        }

        public override string ToString()
        {
            return $"[PlayerHP: PlayerID = {PlayerID} HP = {HP} MaxHP = {MaxHP}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(5);
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
                br.Write(HP);
                br.Write(MaxHP);
            }
        }

        #endregion

    }
}
