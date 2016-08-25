using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The SyncEmoteBubble (0x5B) packet.
    /// </summary>
    public class SyncEmoteBubble : TerrariaPacket
    {

        public int EmoteID { get; set; }

        public byte AnchorType { get; set; }

        public ushort MetaData { get; set; }

        public byte LifeTime { get; set; }

        public byte Emote { get; set; }

        /// <summary>
        /// Only sent if Emote is less than 0
        /// </summary>
        public short MetaData2 { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncEmoteBubble"/> class.
        /// </summary>
        public SyncEmoteBubble() 
            : base((byte)PacketTypes.SyncEmoteBubble)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncEmoteBubble"/> class.
        /// </summary>
        /// <param name="br"></param>
        public SyncEmoteBubble(BinaryReader br) 
            : base(br)
        {
            this.EmoteID = br.ReadInt32(); 
            this.AnchorType = br.ReadByte(); 
            this.MetaData = br.ReadUInt16(); 
            this.LifeTime = br.ReadByte(); 
            this.Emote = br.ReadByte(); 
            this.MetaData2 = br.ReadInt16(); 
        }

        public override string ToString()
        {
            return $"[SyncEmoteBubble: EmoteID = {EmoteID} AnchorType = {AnchorType} MetaData = {MetaData} LifeTime = {LifeTime} Emote = {Emote} MetaData2 = {MetaData2}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(11);
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
                br.Write(EmoteID);
                br.Write(AnchorType);
                br.Write(MetaData);
                br.Write(LifeTime);
                br.Write(Emote);
                br.Write(MetaData2);
            }
        }

        #endregion


    }
}
