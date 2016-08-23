using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The SpecialNPCEffect (0x33) packet.
    /// </summary>
    public class SpecialNPCEffect : TerrariaPacket
    {

        public byte PlayerID { get; set; }

        /// <summary>
        /// Gets or sets the Type - Values: 1 = Spawn Skeletron, 2 = Cause sound at player, 3 = Start Sundialing (Only works if server is receiving), 4 = BigMimcSpawnSmoke|
        /// </summary>
        public byte Type { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecialNPCEffect"/> class.
        /// </summary>
        public SpecialNPCEffect()
            : base((byte)PacketTypes.SpecialNPCEffect)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecialNPCEffect"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public SpecialNPCEffect(BinaryReader br)
            : base(br)
        {
            this.PlayerID = br.ReadByte();
            this.Type = br.ReadByte();
        }

        public override string ToString()
        {
            return $"[SpecialNPCEffect: PlayerID = {PlayerID} Type = {Type}]";
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
                br.Write(Type);
            }
        }

        #endregion

    }
}
