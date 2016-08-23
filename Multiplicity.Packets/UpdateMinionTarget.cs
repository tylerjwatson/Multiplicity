using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The UpdateMinionTarget (0x63) packet.
    /// </summary>
    public class UpdateMinionTarget : TerrariaPacket
    {

        public byte PlayerID { get; set; }

        public float TargetX { get; set; }

        public float TargetY { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateMinionTarget"/> class.
        /// </summary>
        public UpdateMinionTarget()
            : base((byte)PacketTypes.UpdateMinionTarget)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateMinionTarget"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public UpdateMinionTarget(BinaryReader br)
            : base(br)
        {
            this.PlayerID = br.ReadByte();
            this.TargetX = br.ReadSingle();
            this.TargetY = br.ReadSingle();
        }

        public override string ToString()
        {
            return $"[UpdateMinionTarget: PlayerID = {PlayerID} TargetX = {TargetX} TargetY = {TargetY}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(9);
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
                br.Write(TargetX);
                br.Write(TargetY);
            }
        }

        #endregion

    }
}
