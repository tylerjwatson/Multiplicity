using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The DoorToggle (0x13) packet.
    /// </summary>
    public class DoorToggle : TerrariaPacket
    {

        /// <summary>
        /// Gets or sets the Action - 0 = Open Door, 1 = Close Door, 2 = Open Trapdoor, 3 = Close Trapdoor, 4 = Open Tall Gate, 5 = Close Tall Gate|
        /// </summary>
        public byte Action { get; set; }

        public short TileX { get; set; }

        public short TileY { get; set; }

        /// <summary>
        /// Gets or sets the Direction - If (Action == 0) then (if (Direction == -1) then OpenToLeft else OpenToRight) if (Action == 2) then (if (Direction == 1) then PlayerIsAboveTrapdoor) if (Action == 3) then (if (Direction == 1) then PlayerIsAboveTrapdoor)|
        /// </summary>
        public byte Direction { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DoorToggle"/> class.
        /// </summary>
        public DoorToggle()
            : base((byte)PacketTypes.DoorToggle)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DoorToggle"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public DoorToggle(BinaryReader br)
            : base(br)
        {
            this.Action = br.ReadByte();
            this.TileX = br.ReadInt16();
            this.TileY = br.ReadInt16();
            this.Direction = br.ReadByte();
        }

        public override string ToString()
        {
            return $"[DoorToggle: Action = {Action} TileX = {TileX} TileY = {TileY} Direction = {Direction}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(6);
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
                br.Write(Action);
                br.Write(TileX);
                br.Write(TileY);
                br.Write(Direction);
            }
        }

        #endregion

    }
}
