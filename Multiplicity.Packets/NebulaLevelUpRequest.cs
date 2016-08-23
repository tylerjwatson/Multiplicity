using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The NebulaLevelUpRequest (0x66) packet.
    /// </summary>
    public class NebulaLevelUpRequest : TerrariaPacket
    {

        public byte PlayerID { get; set; }

        public byte LevelUpType { get; set; }

        /// <summary>
        /// Gets or sets the OriginX - In world coordinate pixels.|
        /// </summary>
        public float OriginX { get; set; }

        /// <summary>
        /// Gets or sets the OriginY - In world coordinate pixels.|
        /// </summary>
        public float OriginY { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NebulaLevelUpRequest"/> class.
        /// </summary>
        public NebulaLevelUpRequest()
            : base((byte)PacketTypes.NebulaLevelUpRequest)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NebulaLevelUpRequest"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public NebulaLevelUpRequest(BinaryReader br)
            : base(br)
        {
            this.PlayerID = br.ReadByte();
            this.LevelUpType = br.ReadByte();
            this.OriginX = br.ReadSingle();
            this.OriginY = br.ReadSingle();
        }

        public override string ToString()
        {
            return
	            $"[NebulaLevelUpRequest: PlayerID = {PlayerID} LevelUpType = {LevelUpType} OriginX = {OriginX} OriginY = {OriginY}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(10);
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
                br.Write(LevelUpType);
                br.Write(OriginX);
                br.Write(OriginY);
            }
        }

        #endregion

    }
}
