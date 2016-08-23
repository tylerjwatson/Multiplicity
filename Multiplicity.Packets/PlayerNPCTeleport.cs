using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The PlayerNPCTeleport (0x41) packet.
    /// </summary>
    public class PlayerNPCTeleport : TerrariaPacket
    {

        /// <summary>
        /// Gets or sets the Flags - BitFlags: 0 = Player Teleport (Neither 1 or 2), 1 = NPC Teleport, 2 = Player Teleport to Other Player, 4 = Style += 1, 8 = Style += 2|
        /// </summary>
        public byte Flags { get; set; }

        public short TargetID { get; set; }

        public float X { get; set; }

        public float Y { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerNPCTeleport"/> class.
        /// </summary>
        public PlayerNPCTeleport()
            : base((byte)PacketTypes.PlayerNPCTeleport)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerNPCTeleport"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public PlayerNPCTeleport(BinaryReader br)
            : base(br)
        {
            this.Flags = br.ReadByte();
            this.TargetID = br.ReadInt16();
            this.X = br.ReadSingle();
            this.Y = br.ReadSingle();
        }

        public override string ToString()
        {
            return $"[PlayerNPCTeleport: Flags = {Flags} TargetID = {TargetID} X = {X} Y = {Y}]";
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
                br.Write(Flags);
                br.Write(TargetID);
                br.Write(X);
                br.Write(Y);
            }
        }

        #endregion

    }
}
