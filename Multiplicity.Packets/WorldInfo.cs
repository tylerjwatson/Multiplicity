using System;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The WorldInfo (0x7) packet.
    /// </summary>
    public class WorldInfo : TerrariaPacket
    {

        public int Time { get; set; }

        /// <summary>
        /// Gets or sets the DayandMoonInfo - BitFlags: 1 = Day Time, 2 = Blood Moon, 4 = Eclipse|
        /// </summary>
        public byte DayandMoonInfo { get; set; }

        public byte MoonPhase { get; set; }

        public short MaxTilesX { get; set; }

        public short MaxTilesY { get; set; }

        public short SpawnX { get; set; }

        public short SpawnY { get; set; }

        public short WorldSurface { get; set; }

        public short RockLayer { get; set; }

        public int WorldID { get; set; }

        public string WorldName { get; set; }

        public byte MoonType { get; set; }

        public byte TreeBackground { get; set; }

        public byte CorruptionBackground { get; set; }

        public byte JungleBackground { get; set; }

        public byte SnowBackground { get; set; }

        public byte HallowBackground { get; set; }

        public byte CrimsonBackground { get; set; }

        public byte DesertBackground { get; set; }

        public byte OceanBackground { get; set; }

        public byte IceBackStyle { get; set; }

        public byte JungleBackStyle { get; set; }

        public byte HellBackStyle { get; set; }

        public float WindSpeedSet { get; set; }

        public byte CloudNumber { get; set; }

        public int Tree1 { get; set; }

        public int Tree2 { get; set; }

        public int Tree3 { get; set; }

        public byte TreeStyle1 { get; set; }

        public byte TreeStyle2 { get; set; }

        public byte TreeStyle3 { get; set; }

        public byte TreeStyle4 { get; set; }

        public int CaveBack1 { get; set; }

        public int CaveBack2 { get; set; }

        public int CaveBack3 { get; set; }

        public byte CaveBackStyle1 { get; set; }

        public byte CaveBackStyle2 { get; set; }

        public byte CaveBackStyle3 { get; set; }

        public byte CaveBackStyle4 { get; set; }

        public float Rain { get; set; }

        /// <summary>
        /// Gets or sets the EventInfo - BitFlags: 1 = Shadow Orb Smashed, 2 = Downed Boss 1, 4 = Downed Boss 2, 8 = Downed Boss 3, 16 = Hard Mode, 32 = Downed Clown, 64 = Server Side Character, 128 = Downed Plant Boss|
        /// </summary>
        public byte EventInfo { get; set; }

        /// <summary>
        /// Gets or sets the EventInfo2 - BitFlags: 1 = Mech Boss Downed, 2 = Mech Boss Downed 2, 4 = Mech Boss Downed 3, 8 = Mech Boss Any Downed, 16 = Cloud BG, 32 = Crimson, 64 = Pumpkin Moon, 128 = Snow Moon|
        /// </summary>
        public byte EventInfo2 { get; set; }

        public byte EventInfo3 { get; set; }

        public byte EventInfo4 { get; set; }

        public sbyte InvasionType { get; set; }

        public ulong LobbyID { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldInfo"/> class.
        /// </summary>
        public WorldInfo()
            : base((byte)PacketTypes.WorldInfo)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldInfo"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public WorldInfo(BinaryReader br)
            : base(br)
        {
            this.Time = br.ReadInt32();
            this.DayandMoonInfo = br.ReadByte();
            this.MoonPhase = br.ReadByte();
            this.MaxTilesX = br.ReadInt16();
            this.MaxTilesY = br.ReadInt16();
            this.SpawnX = br.ReadInt16();
            this.SpawnY = br.ReadInt16();
            this.WorldSurface = br.ReadInt16();
            this.RockLayer = br.ReadInt16();
            this.WorldID = br.ReadInt32();
            this.WorldName = br.ReadString();
            this.MoonType = br.ReadByte();
            this.TreeBackground = br.ReadByte();
            this.CorruptionBackground = br.ReadByte();
            this.JungleBackground = br.ReadByte();
            this.SnowBackground = br.ReadByte();
            this.HallowBackground = br.ReadByte();
            this.CrimsonBackground = br.ReadByte();
            this.DesertBackground = br.ReadByte();
            this.OceanBackground = br.ReadByte();
            this.IceBackStyle = br.ReadByte();
            this.JungleBackStyle = br.ReadByte();
            this.HellBackStyle = br.ReadByte();
            this.WindSpeedSet = br.ReadSingle();
            this.CloudNumber = br.ReadByte();
            this.Tree1 = br.ReadInt32();
            this.Tree2 = br.ReadInt32();
            this.Tree3 = br.ReadInt32();
            this.TreeStyle1 = br.ReadByte();
            this.TreeStyle2 = br.ReadByte();
            this.TreeStyle3 = br.ReadByte();
            this.TreeStyle4 = br.ReadByte();
            this.CaveBack1 = br.ReadInt32();
            this.CaveBack2 = br.ReadInt32();
            this.CaveBack3 = br.ReadInt32();
            this.CaveBackStyle1 = br.ReadByte();
            this.CaveBackStyle2 = br.ReadByte();
            this.CaveBackStyle3 = br.ReadByte();
            this.CaveBackStyle4 = br.ReadByte();
            this.Rain = br.ReadSingle();
            this.EventInfo = br.ReadByte();
            this.EventInfo2 = br.ReadByte();
            this.EventInfo3 = br.ReadByte();
            this.EventInfo4 = br.ReadByte();
            this.InvasionType = br.ReadSByte();
            this.LobbyID = br.ReadUInt64();
        }

        public override string ToString()
        {
            return
	            $"[WorldInfo: Time = {Time} DayandMoonInfo = {DayandMoonInfo} MoonPhase = {MoonPhase} MaxTilesX = {MaxTilesX} MaxTilesY = {MaxTilesY} SpawnX = {SpawnX} SpawnY = {SpawnY} WorldSurface = {WorldSurface} RockLayer = {RockLayer} WorldID = {WorldID} WorldName = {WorldName} MoonType = {MoonType} TreeBackground = {TreeBackground} CorruptionBackground = {CorruptionBackground} JungleBackground = {JungleBackground} SnowBackground = {SnowBackground} HallowBackground = {HallowBackground} CrimsonBackground = {CrimsonBackground} DesertBackground = {DesertBackground} OceanBackground = {OceanBackground} IceBackStyle = {IceBackStyle} JungleBackStyle = {JungleBackStyle} HellBackStyle = {HellBackStyle} WindSpeedSet = {WindSpeedSet} CloudNumber = {CloudNumber} Tree1 = {Tree1} Tree2 = {Tree2} Tree3 = {Tree3} TreeStyle1 = {TreeStyle1} TreeStyle2 = {TreeStyle2} TreeStyle3 = {TreeStyle3} TreeStyle4 = {TreeStyle4} CaveBack1 = {CaveBack1} CaveBack2 = {CaveBack2} CaveBack3 = {CaveBack3} CaveBackStyle1 = {CaveBackStyle1} CaveBackStyle2 = {CaveBackStyle2} CaveBackStyle3 = {CaveBackStyle3} CaveBackStyle4 = {CaveBackStyle4} Rain = {Rain} EventInfo = {EventInfo} EventInfo2 = {EventInfo2} EventInfo3 = {EventInfo3} EventInfo4 = {EventInfo4} InvasionType = {InvasionType} LobbyID = {LobbyID}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(89 + WorldName.Length);
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
                br.Write(Time);
                br.Write(DayandMoonInfo);
                br.Write(MoonPhase);
                br.Write(MaxTilesX);
                br.Write(MaxTilesY);
                br.Write(SpawnX);
                br.Write(SpawnY);
                br.Write(WorldSurface);
                br.Write(RockLayer);
                br.Write(WorldID);
                br.Write(WorldName);
                br.Write(MoonType);
                br.Write(TreeBackground);
                br.Write(CorruptionBackground);
                br.Write(JungleBackground);
                br.Write(SnowBackground);
                br.Write(HallowBackground);
                br.Write(CrimsonBackground);
                br.Write(DesertBackground);
                br.Write(OceanBackground);
                br.Write(IceBackStyle);
                br.Write(JungleBackStyle);
                br.Write(HellBackStyle);
                br.Write(WindSpeedSet);
                br.Write(CloudNumber);
                br.Write(Tree1);
                br.Write(Tree2);
                br.Write(Tree3);
                br.Write(TreeStyle1);
                br.Write(TreeStyle2);
                br.Write(TreeStyle3);
                br.Write(TreeStyle4);
                br.Write(CaveBack1);
                br.Write(CaveBack2);
                br.Write(CaveBack3);
                br.Write(CaveBackStyle1);
                br.Write(CaveBackStyle2);
                br.Write(CaveBackStyle3);
                br.Write(CaveBackStyle4);
                br.Write(Rain);
                br.Write(EventInfo);
                br.Write(EventInfo2);
                br.Write(EventInfo3);
                br.Write(EventInfo4);
                br.Write(InvasionType);
                br.Write(LobbyID);
            }
        }

        #endregion

    }
}
