using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The DestroyProjectile (0x1D) packet.
    /// </summary>
    public class DestroyProjectile : TerrariaPacket
    {

        public short ProjectileID { get; set; }

        /// <summary>
        /// Gets or sets the Owner - Player ID|
        /// </summary>
        public byte Owner { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DestroyProjectile"/> class.
        /// </summary>
        public DestroyProjectile()
            : base((byte)PacketTypes.DestroyProjectile)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DestroyProjectile"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public DestroyProjectile(BinaryReader br)
            : base(br)
        {
            this.ProjectileID = br.ReadInt16();
            this.Owner = br.ReadByte();
        }

        public override string ToString()
        {
            return $"[DestroyProjectile: ProjectileID = {ProjectileID} Owner = {Owner}]";
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
                br.Write(ProjectileID);
                br.Write(Owner);
            }
        }

        #endregion

    }
}
