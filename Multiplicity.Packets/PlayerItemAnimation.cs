using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The PlayerItemAnimation (0x29) packet.
    /// </summary>
    public class PlayerItemAnimation : TerrariaPacket
    {

        public byte PlayerID { get; set; }

        public float ItemRotation { get; set; }

        public short ItemAnimation { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerItemAnimation"/> class.
        /// </summary>
        public PlayerItemAnimation()
            : base((byte)PacketTypes.PlayerItemAnimation)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerItemAnimation"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public PlayerItemAnimation(BinaryReader br)
            : base(br)
        {
            this.PlayerID = br.ReadByte();
            this.ItemRotation = br.ReadSingle();
            this.ItemAnimation = br.ReadInt16();
        }

        public override string ToString()
        {
            return
	            $"[PlayerItemAnimation: PlayerID = {PlayerID} ItemRotation = {ItemRotation} ItemAnimation = {ItemAnimation}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(7);
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
                br.Write(ItemRotation);
                br.Write(ItemAnimation);
            }
        }

        #endregion

    }
}
