using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The PlayerMana (0x2A) packet.
    /// </summary>
    public class PlayerMana : TerrariaPacket
    {

        public byte PlayerID { get; set; }

        public short Mana { get; set; }

        public short MaxMana { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerMana"/> class.
        /// </summary>
        public PlayerMana()
            : base((byte)PacketTypes.PlayerMana)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerMana"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public PlayerMana(BinaryReader br)
            : base(br)
        {
            this.PlayerID = br.ReadByte();
            this.Mana = br.ReadInt16();
            this.MaxMana = br.ReadInt16();
        }

        public override string ToString()
        {
            return $"[PlayerMana: PlayerID = {PlayerID} Mana = {Mana} MaxMana = {MaxMana}]";
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
                br.Write(Mana);
                br.Write(MaxMana);
            }
        }

        #endregion

    }
}
