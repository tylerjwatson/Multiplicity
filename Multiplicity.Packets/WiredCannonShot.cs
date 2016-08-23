using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The WiredCannonShot (0x6C) packet.
    /// </summary>
    public class WiredCannonShot : TerrariaPacket
    {

        public short Damage { get; set; }

        public float Knockback { get; set; }

        public short X { get; set; }

        public short Y { get; set; }

        public short Angle { get; set; }

        public short Ammo { get; set; }

        /// <summary>
        /// Gets or sets the PlayerID - Shooter's Player ID|
        /// </summary>
        public byte PlayerID { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WiredCannonShot"/> class.
        /// </summary>
        public WiredCannonShot()
            : base((byte)PacketTypes.WiredCannonShot)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WiredCannonShot"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public WiredCannonShot(BinaryReader br)
            : base(br)
        {
            this.Damage = br.ReadInt16();
            this.Knockback = br.ReadSingle();
            this.X = br.ReadInt16();
            this.Y = br.ReadInt16();
            this.Angle = br.ReadInt16();
            this.Ammo = br.ReadInt16();
            this.PlayerID = br.ReadByte();
        }

        public override string ToString()
        {
            return
	            $"[WiredCannonShot: Damage = {Damage} Knockback = {Knockback} X = {X} Y = {Y} Angle = {Angle} Ammo = {Ammo} PlayerID = {PlayerID}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(15);
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
                br.Write(Damage);
                br.Write(Knockback);
                br.Write(X);
                br.Write(Y);
                br.Write(Angle);
                br.Write(Ammo);
                br.Write(PlayerID);
            }
        }

        #endregion

    }
}
