using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The ModifyTile (0x11) packet.
    /// </summary>
    public class ModifyTile : TerrariaPacket
    {

        /// <summary>
        /// Gets or sets the Action - 0 = Kill Tile, 1 = Place Tile, 2 = Kill Wall, 3 = Place Wall, 4 = Kill Tile No Item, 5 = Place Wire
        /// 6 = Kill Wire, 7 = Pound Tile, 8 = Place Actuator, 9 = Kill Actuator, 10 = Place Wire2, 11 = Kill Wire2, 12 = Place Wire3, 13 = Kill Wire3
        /// 14 = Slope Tile, 15 = Frame Track, 16 = Place Wire4, 17 = Kill Wire4, 18 = Poke Logic Gate, 19 = Actuate
        /// </summary>
        public byte Action { get; set; }

        public short TileX { get; set; }

        public short TileY { get; set; }

        public short EditData { get; set; }

        public byte Style { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModifyTile"/> class.
        /// </summary>
        public ModifyTile() : base((byte)PacketTypes.ModifyTile)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModifyTile"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public ModifyTile(BinaryReader br) : base(br)
        {
            this.Action = br.ReadByte();
            this.TileX = br.ReadInt16();
            this.TileY = br.ReadInt16();
            this.EditData = br.ReadInt16();
            this.Style = br.ReadByte();
        }

        public override string ToString()
        {
            return $"[ModifyTile: Action = {Action} TileX = {TileX} TileY = {TileY} EditData = {EditData} Style = {Style}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(8);
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
                br.Write(EditData);
                br.Write(Style);
            }
        }

        #endregion

    }
}
