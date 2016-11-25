using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Multiplicity.Packets
{
	/// <summary>
	/// The PlayerHurtV2 (75) packet.
	/// </summary>
	public class PlayerHurtV2 : TerrariaPacket
	{
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
		public short FromItemPrefix { get; set; }

		public short Damage { get; set; }

		public byte HitDirection { get; set; }

		/// <summary>
		/// BitFlags: 1 = Crit, 2 = PvP
		/// </summary>
		public byte Flags { get; set; }

		public byte CooldownCounter { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="PlayerHurtV2"/> class.
		/// </summary>
		public PlayerHurtV2()
			: base((byte)PacketTypes.PlayerHurtV2)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PlayerHurtV2"/> class.
		/// </summary>
		/// <param name="br">br</param>
		public PlayerHurtV2(BinaryReader br)
			: base (br)
		{
			PlayerId = br.ReadByte();
			PlayerDeathReason = br.ReadByte();
			FromPlayerIndex = br.ReadInt16();
			FromNpcIndex = br.ReadInt16();
			FromProjectileIndex = br.ReadInt16();
			FromOther = br.ReadByte();
			FromProjectileType = br.ReadInt16();
			FromItemType = br.ReadInt16();
			FromItemPrefix = br.ReadInt16();
			Damage = br.ReadInt16();
			HitDirection = br.ReadByte();
			Flags = br.ReadByte();
			CooldownCounter = br.ReadByte();
		}

		public override string ToString()
		{
			return 
				$"[PlayerHurtV2: PlayerId = {PlayerId} PlayerDeathReason = {PlayerDeathReason} FromPlayerIndex = {FromPlayerIndex} FromNpcIndex = {FromNpcIndex} FromProjectileIndex = {FromProjectileIndex} FromOther = {FromOther} FromProjectileType = {FromProjectileType} FromItemType = {FromItemType} FromItemPrefix = {FromItemPrefix} Damage = {Damage} HitDirection = {HitDirection} Flags = {Flags} CooldownCounter = {CooldownCounter}]";
		}

		#region implemented abstract members of TerrariaPacket

		public override short GetLength()
		{
			return (short)(20);
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
				br.Write(CooldownCounter);
			}
		}

		#endregion
	}
}
