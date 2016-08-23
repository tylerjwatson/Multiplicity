using System.Drawing;
using System.IO;
using Multiplicity.Packets.Extensions;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The PlayerInfo (0x4) packet.
    /// </summary>
    public class PlayerInfo : TerrariaPacket
    {

        public byte PlayerID { get; set; }

        public byte SkinVarient { get; set; }

        /// <summary>
        /// Gets or sets the Hair - If >134 then Set To 0|
        /// </summary>
        public byte Hair { get; set; }

        public string Name { get; set; }

        public byte HairDye { get; set; }

        public byte HideVisuals { get; set; }

        public byte HideVisuals2 { get; set; }

        public byte HideMisc { get; set; }

        public Color HairColor { get; set; }

        public Color SkinColor { get; set; }

        public Color EyeColor { get; set; }

        public Color ShirtColor { get; set; }

        public Color UnderShirtColor { get; set; }

        public Color PantsColor { get; set; }

        public Color ShoeColor { get; set; }

        public byte Difficulty { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerInfo"/> class.
        /// </summary>
        public PlayerInfo()
            : base((byte)PacketTypes.PlayerInfo)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerInfo"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public PlayerInfo(BinaryReader br)
            : base(br)
        {
            this.PlayerID = br.ReadByte();
            this.SkinVarient = br.ReadByte();
            this.Hair = br.ReadByte();
            this.Name = br.ReadString();
            this.HairDye = br.ReadByte();
            this.HideVisuals = br.ReadByte();
            this.HideVisuals2 = br.ReadByte();
            this.HideMisc = br.ReadByte();
            this.HairColor = br.ReadColor();
            this.SkinColor = br.ReadColor();
            this.EyeColor = br.ReadColor();
            this.ShirtColor = br.ReadColor();
            this.UnderShirtColor = br.ReadColor();
            this.PantsColor = br.ReadColor();
            this.ShoeColor = br.ReadColor();
            this.Difficulty = br.ReadByte();
        }

        public override string ToString()
        {
            return
	            $"[PlayerInfo: PlayerID = {PlayerID} SkinVarient = {SkinVarient} Hair = {Hair} Name = {Name} HairDye = {HairDye} HideVisuals = {HideVisuals} HideVisuals2 = {HideVisuals2} HideMisc = {HideMisc} HairColor = {HairColor} SkinColor = {SkinColor} EyeColor = {EyeColor} ShirtColor = {ShirtColor} UnderShirtColor = {UnderShirtColor} PantsColor = {PantsColor} ShoeColor = {ShoeColor} Difficulty = {Difficulty}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(30 + Name.Length);
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
                br.Write(SkinVarient);
                br.Write(Hair);
                br.Write(Name);
                br.Write(HairDye);
                br.Write(HideVisuals);
                br.Write(HideVisuals2);
                br.Write(HideMisc);
                br.Write(HairColor);
                br.Write(SkinColor);
                br.Write(EyeColor);
                br.Write(ShirtColor);
                br.Write(UnderShirtColor);
                br.Write(PantsColor);
                br.Write(ShoeColor);
                br.Write(Difficulty);
            }
        }

        #endregion

    }
}
