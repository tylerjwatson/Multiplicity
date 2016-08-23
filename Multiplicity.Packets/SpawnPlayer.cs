using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The SpawnPlayer (0xC) packet.
    /// </summary>
    public class SpawnPlayer : TerrariaPacket
    {

        public byte PlayerID { get; set; }

        public short SpawnX { get; set; }

        public short SpawnY { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpawnPlayer"/> class.
        /// </summary>
        public SpawnPlayer()
            : base((byte)PacketTypes.SpawnPlayer)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpawnPlayer"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public SpawnPlayer(BinaryReader br)
            : base(br)
        {
            this.PlayerID = br.ReadByte();
            this.SpawnX = br.ReadInt16();
            this.SpawnY = br.ReadInt16();
        }

        public override string ToString()
        {
            return $"[SpawnPlayer: PlayerID = {PlayerID} SpawnX = {SpawnX} SpawnY = {SpawnY}]";
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
                br.Write(PlayerID);
                br.Write(SpawnX);
                br.Write(SpawnY);
            }
        }

        #endregion

    }
}
