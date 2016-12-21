using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The CrystalInvasionSendWaitTime (74) packet.
    /// </summary>
    public class CrystalInvasionSendWaitTime : TerrariaPacket
    {
        public int NextWaveTime { get; set; }

        public CrystalInvasionSendWaitTime()
            : base((byte)PacketTypes.CrystalInvasionSendWaitTime)
        {

        }

        public CrystalInvasionSendWaitTime(BinaryReader br)
            : base(br)
        {
            NextWaveTime = br.ReadInt32();
        }

        public override string ToString()
        {
            return $"[CrystalInvasionSendWaitTime: NextWaveTime = {NextWaveTime}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(4);
        }

        public override void ToStream(Stream stream, bool includeHeader = true)
        {
            /*
             * Length and ID headers get written in the base packet class.
             */
            if (includeHeader)
            {
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
            using (BinaryWriter br = new BinaryWriter(stream, new System.Text.UTF8Encoding(), leaveOpen: true))
            {
                br.Write(NextWaveTime);
            }
        }

        #endregion
    }
}
