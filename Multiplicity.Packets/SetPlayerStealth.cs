using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The SetPlayerStealth (0x54) packet.
    /// </summary>
    public class SetPlayerStealth : TerrariaPacket
    {

        public byte Player { get; set; }

        public float Stealth { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SetPlayerStealth"/> class.
        /// </summary>
        public SetPlayerStealth()
            : base((byte)PacketTypes.SetPlayerStealth)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SetPlayerStealth"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public SetPlayerStealth(BinaryReader br)
            : base(br)
        {
            this.Player = br.ReadByte();
            this.Stealth = br.ReadSingle();
        }

        public override string ToString()
        {
            return $"[SetPlayerStealth: Player = {Player} Stealth = {Stealth}]";
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
                br.Write(Player);
                br.Write(Stealth);
            }
        }

        #endregion

    }
}
