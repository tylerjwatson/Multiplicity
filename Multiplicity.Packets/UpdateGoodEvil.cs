using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The UpdateGoodEvil (0x39) packet.
    /// </summary>
    public class UpdateGoodEvil : TerrariaPacket
    {

        public byte Good { get; set; }

        public byte Evil { get; set; }

        public byte Crimson { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateGoodEvil"/> class.
        /// </summary>
        public UpdateGoodEvil()
            : base((byte)PacketTypes.UpdateGoodEvil)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateGoodEvil"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public UpdateGoodEvil(BinaryReader br)
            : base(br)
        {
            this.Good = br.ReadByte();
            this.Evil = br.ReadByte();
            this.Crimson = br.ReadByte();
        }

        public override string ToString()
        {
            return $"[UpdateGoodEvil: Good = {Good} Evil = {Evil} Crimson = {Crimson}]";
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
                br.Write(Good);
                br.Write(Evil);
                br.Write(Crimson);
            }
        }

        #endregion

    }
}
