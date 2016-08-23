using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The PlayerDamage (0x1A) packet.
    /// </summary>
    public class PlayerDamage : TerrariaPacket
    {

        public byte PlayerID { get; set; }

        public byte HitDirection { get; set; }

        public short Damage { get; set; }

        public string DeathText { get; set; }

        /// <summary>
        /// Gets or sets the Flags - BitFlags: 1 = PVP, 2 = Crit, 4 = CooldownCountdown Is -1, 8 = CooldownCountdown is 1 (Overrides previous flag)|
        /// </summary>
        public byte Flags { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerDamage"/> class.
        /// </summary>
        public PlayerDamage()
            : base((byte)PacketTypes.PlayerDamage)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerDamage"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public PlayerDamage(BinaryReader br)
            : base(br)
        {
            this.PlayerID = br.ReadByte();
            this.HitDirection = br.ReadByte();
            this.Damage = br.ReadInt16();
            this.DeathText = br.ReadString();
            this.Flags = br.ReadByte();
        }

        public override string ToString()
        {
            return
	            $"[PlayerDamage: PlayerID = {PlayerID} HitDirection = {HitDirection} Damage = {Damage} DeathText = {DeathText} Flags = {Flags}]";
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
                br.Write(DeathText);
                br.Write(Flags);
            }
        }

        #endregion

    }
}
