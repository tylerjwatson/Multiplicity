using System.IO;
using Multiplicity.Packets.Extensions;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The PlayerDeathV2 (76) packet.
    /// </summary>
    public class PlayerDeathV2 : TerrariaPacket
    {
        private int _length;

        public byte PlayerId { get; set; }

        /// <summary>
        /// BitFlags: 1 = From Player Index, 2 = From NPC Index, 4 = From Projectile Index
        /// 8 = From other, 16 = From Projectile Type, 32 = From Item Type, 64 = From Item Prefix
        /// </summary>
        public byte PlayerDeathReason { get; set; }

        /// <summary>
        /// Only in PvP.
        /// </summary>
        public short FromPlayerIndex { get; set; }

        /// <summary>
        /// Only if hurt by an npc.
        /// </summary>
        public short FromNpcIndex { get; set; }

        /// <summary>
        /// Only in PvP.
        /// </summary>
        public short FromProjectileIndex { get; set; }

        /// <summary>
        /// 0 = Fall damage, 1 = Drowning, 2 = Lava damage, 3 = Fall damage, 4 = Demon Altar,
        /// 5 = N/A, 6 = Companion Cube, 7 = Suffocation, 8 = Burning, 9 = Poison/Venom,
        /// 10 = Electrified, 11 = WoF (escaped), 12 = WoF (licked), 13 = Chaos State,
        /// 14 = Chaos State V2 (male), 15 = Chaos State V2 (female)
        /// </summary>
        public byte FromOther { get; set; }

        /// <summary>
        /// Only in PvP.
        /// </summary>
        public short FromProjectileType { get; set; }

        /// <summary>
        /// Only in PvP.
        /// </summary>
        public short FromItemType { get; set; }

        /// <summary>
        /// Only in PvP.
        /// </summary>
        public byte FromItemPrefix { get; set; }

        public string FromCustomReason { get; set; }

        public short Damage { get; set; }

        public byte HitDirection { get; set; }

        /// <summary>
        /// BitFlags: 1 = PvP
        /// </summary>
        public byte Flags { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerHurtV2"/> class.
        /// </summary>
        public PlayerDeathV2()
            : base((byte)PacketTypes.PlayerDeathV2)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerHurtV2"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public PlayerDeathV2(BinaryReader br)
            : base(br)
        {
            PlayerId = br.ReadByte();
            PlayerDeathReason = br.ReadByte();

            if (PlayerDeathReason.ReadBit(0))
            {
                FromPlayerIndex = br.ReadInt16();
                _length += 2;
            }

            if (PlayerDeathReason.ReadBit(1))
            {
                FromNpcIndex = br.ReadInt16();
                _length += 2;
            }

            if (PlayerDeathReason.ReadBit(2))
            {
                FromProjectileIndex = br.ReadInt16();
                _length += 2;
            }

            if (PlayerDeathReason.ReadBit(3))
            {
                FromOther = br.ReadByte();
                _length += 1;
            }

            if (PlayerDeathReason.ReadBit(4))
            {
                FromProjectileType = br.ReadInt16();
                _length += 2;
            }

            if (PlayerDeathReason.ReadBit(5))
            {
                FromItemType = br.ReadInt16();
                _length += 2;
            }

            if (PlayerDeathReason.ReadBit(6))
            {
                FromItemPrefix = br.ReadByte();
                _length += 1;
            }

            if (PlayerDeathReason.ReadBit(7))
            {
                FromCustomReason = br.ReadString();
                _length += FromCustomReason.Length;
            }

            Damage = br.ReadInt16();
            HitDirection = br.ReadByte();
            Flags = br.ReadByte();
        }

        public override string ToString()
        {
            return
                $"[PlayerDeathV2: PlayerId = {PlayerId} PlayerDeathReason = {PlayerDeathReason} FromPlayerIndex = {FromPlayerIndex} FromNpcIndex = {FromNpcIndex} FromProjectileIndex = {FromProjectileIndex} FromOther = {FromOther} FromProjectileType = {FromProjectileType} FromItemType = {FromItemType} FromItemPrefix = {FromItemPrefix} FromCustomReason = {FromCustomReason} Damage = {Damage} HitDirection = {HitDirection} Flags = {Flags}]";
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(6 + _length);
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
                br.Write(PlayerId);
                br.Write(PlayerDeathReason);
                br.Write(FromPlayerIndex);
                br.Write(FromNpcIndex);
                br.Write(FromProjectileIndex);
                br.Write(FromOther);
                br.Write(FromProjectileType);
                br.Write(FromItemType);
                br.Write(FromItemPrefix);
                br.Write(Damage);
                br.Write(HitDirection);
                br.Write(Flags);
            }
        }

        #endregion
    }
}
