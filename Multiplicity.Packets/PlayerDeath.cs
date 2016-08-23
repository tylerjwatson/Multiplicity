using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The PlayerDeath (0x2C) packet.
    /// </summary>
    public class PlayerDeath : TerrariaPacket
    {

        public byte PlayerID { get; set; }

        public byte HitDirection { get; set; }

        public short Damage { get; set; }

        public bool PVP { get; set; }

        public string DeathText { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerDeath"/> class.
        /// </summary>
        public PlayerDeath()
            : base((byte)PacketTypes.PlayerDeath)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerDeath"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public PlayerDeath(BinaryReader br)
            : base(br)
        {
            this.PlayerID = br.ReadByte();
            this.HitDirection = br.ReadByte();
            this.Damage = br.ReadInt16();
            this.PVP = br.ReadBoolean();
            this.DeathText = br.ReadString();
        }

        public override string ToString()
        {
            return
	            $"[PlayerDeath: PlayerID = {PlayerID} HitDirection = {HitDirection} Damage = {Damage} PVP = {PVP} DeathText = {DeathText}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(6 + DeathText.Length);
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
                br.Write(HitDirection);
                br.Write(Damage);
                br.Write(PVP);
                br.Write(DeathText);
            }
        }

        #endregion

    }
}
