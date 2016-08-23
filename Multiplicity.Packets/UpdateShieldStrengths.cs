using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The UpdateShieldStrengths (0x65) packet.
    /// </summary>
    public class UpdateShieldStrengths : TerrariaPacket
    {

        public ushort SolarTowerShield { get; set; }

        public ushort VortexTowerShield { get; set; }

        public ushort NebulaTowerShield { get; set; }

        public ushort StardustTowerShield { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateShieldStrengths"/> class.
        /// </summary>
        public UpdateShieldStrengths()
            : base((byte)PacketTypes.UpdateShieldStrengths)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateShieldStrengths"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public UpdateShieldStrengths(BinaryReader br)
            : base(br)
        {
            this.SolarTowerShield = br.ReadUInt16();
            this.VortexTowerShield = br.ReadUInt16();
            this.NebulaTowerShield = br.ReadUInt16();
            this.StardustTowerShield = br.ReadUInt16();
        }

        public override string ToString()
        {
            return
	            $"[UpdateShieldStrengths: SolarTowerShield = {SolarTowerShield} VortexTowerShield = {VortexTowerShield} NebulaTowerShield = {NebulaTowerShield} StardustTowerShield = {StardustTowerShield}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(8);
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
                br.Write(SolarTowerShield);
                br.Write(VortexTowerShield);
                br.Write(NebulaTowerShield);
                br.Write(StardustTowerShield);
            }
        }

        #endregion

    }
}
