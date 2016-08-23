using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The ReportInvasionProgress (0x4E) packet.
    /// </summary>
    public class ReportInvasionProgress : TerrariaPacket
    {

        public int Progress { get; set; }

        public int MaxProgress { get; set; }

        public sbyte Icon { get; set; }

        public sbyte Wave { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportInvasionProgress"/> class.
        /// </summary>
        public ReportInvasionProgress()
            : base((byte)PacketTypes.ReportInvasionProgress)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportInvasionProgress"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public ReportInvasionProgress(BinaryReader br)
            : base(br)
        {
            this.Progress = br.ReadInt32();
            this.MaxProgress = br.ReadInt32();
            this.Icon = br.ReadSByte();
            this.Wave = br.ReadSByte();
        }

        public override string ToString()
        {
            return
	            $"[ReportInvasionProgress: Progress = {Progress} MaxProgress = {MaxProgress} Icon = {Icon} Wave = {Wave}]";
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
                br.Write(Progress);
                br.Write(MaxProgress);
                br.Write(Icon);
                br.Write(Wave);
            }
        }

        #endregion

    }
}
