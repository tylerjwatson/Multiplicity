using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The PlayerTeam (0x2D) packet.
    /// </summary>
    public class PlayerTeam : TerrariaPacket
    {

        public byte PlayerID { get; set; }

        public byte Team { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerTeam"/> class.
        /// </summary>
        public PlayerTeam()
            : base((byte)PacketTypes.PlayerTeam)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerTeam"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public PlayerTeam(BinaryReader br)
            : base(br)
        {
            this.PlayerID = br.ReadByte();
            this.Team = br.ReadByte();
        }

        public override string ToString()
        {
            return $"[PlayerTeam: PlayerID = {PlayerID} Team = {Team}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(2);
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
                br.Write(Team);
            }
        }

        #endregion

    }
}
