using System;
using System.Collections.Generic;
using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// Abstract base class generically representing a terraria packet.
    /// </summary>
    public abstract class TerrariaPacket : TerrariaNetworkObject
    {
        public const short PACKET_HEADER_LEN = 3;
        public byte[] TestRawBuffer { get; set; }

        protected short _length = 0;

        /// <summary>
        /// The deserializer map.
        /// 
        /// Deserializer maps point to a function to return a fully qualified packet
        /// from one supplied BinaryReader object.  Derivatives of TerrariaPacket
        /// should make sure that they return a valid packet structure when passed a
        /// BinaryReader to deserialize from.
        /// </summary>
        public static Dictionary<PacketTypes, Func<BinaryReader, TerrariaPacket>> deserializerMap = new Dictionary<PacketTypes, Func<BinaryReader, TerrariaPacket>>() {
            /*001*/ { PacketTypes.ConnectRequest, (br) => new ConnectRequest(br) },
            /*002*/ { PacketTypes.Disconnect, (br) => new Disconnect(br) },
            /*003*/ { PacketTypes.ContinueConnecting, (br) => new ContinueConnecting(br) },
            /*004*/ { PacketTypes.PlayerInfo, (br) => new PlayerInfo(br) },
            /*005*/ { PacketTypes.PlayerInventorySlot, (br) => new PlayerInventorySlot(br) },
            /*006*/ { PacketTypes.ContinueConnecting2, (br) => new ContinueConnecting2(br) },
            /*007*/ { PacketTypes.WorldInfo, (br) => new WorldInfo(br) },
            /*008*/ { PacketTypes.GetSection, (br) => new GetSection(br) },
            /*009*/ { PacketTypes.Status, (br) => new Status(br) },
            /*010*/ { PacketTypes.SendSection, (br) => new SendSection(br) },
            /*011*/ { PacketTypes.SectionTileFrame, (br) => new SectionTileFrame(br) },
            /*012*/ { PacketTypes.SpawnPlayer, (br) => new SpawnPlayer(br) },
            /*013*/ { PacketTypes.UpdatePlayer, (br) => new UpdatePlayer(br) },
            /*014*/ { PacketTypes.PlayerActive, (br) => new PlayerActive(br) },
            /*015*/ { PacketTypes.Null, (br) => new Null(br) },
            /*016*/ { PacketTypes.PlayerHP, (br) => new PlayerHP(br) },
            /*017*/ { PacketTypes.ModifyTile, (br) => new ModifyTile(br) },
            /*018*/ { PacketTypes.Time, (br) => new Time(br) },
            /*019*/ { PacketTypes.DoorToggle, (br) => new DoorToggle(br) },
            /*020*/ { PacketTypes.SendTileSquare, (br) => new SendTileSquare(br) },
            /*021*/ { PacketTypes.UpdateItemDrop, (br) => new UpdateItemDrop(br) },
            /*022*/ { PacketTypes.UpdateItemOwner, (br) => new UpdateItemOwner(br) },
            /*023*/ { PacketTypes.NPCUpdate, (br) => new NPCUpdate(br) },
            /*024*/ { PacketTypes.StrikeNPCwithHeldItem, (br) => new StrikeNPCwithHeldItem(br) },
            /*025*/ { PacketTypes.ChatMessage, (br) => new ChatMessage(br) },
            /*026*/ { PacketTypes.PlayerDamage, (br) => new PlayerDamage(br) },
            /*027*/ { PacketTypes.ProjectileUpdate, (br) => new ProjectileUpdate(br) },
            /*028*/ { PacketTypes.NPCStrike, (br) => new NPCStrike(br) },
            /*029*/ { PacketTypes.DestroyProjectile, (br) => new DestroyProjectile(br) },
            /*030*/ { PacketTypes.TogglePVP, (br) => new TogglePVP(br) },
            /*031*/ { PacketTypes.GetChestContents, (br) => new GetChestContents(br) },
            /*032*/ { PacketTypes.ChestItem, (br) => new ChestItem(br) },
            /*033*/ { PacketTypes.SetChestName, (br) => new SetChestName(br) },
            /*034*/ { PacketTypes.PlaceChest, (br) => new PlaceChest(br) },
            /*035*/ { PacketTypes.HealEffect, (br) => new HealEffect(br) },
            /*036*/ { PacketTypes.PlayerZone, (br) => new PlayerZone(br) },
            /*037*/ { PacketTypes.RequestPassword, (br) => new RequestPassword(br) },
            /*038*/ { PacketTypes.SendPassword, (br) => new SendPassword(br) },
            /*039*/ { PacketTypes.RemoveItemOwner, (br) => new RemoveItemOwner(br) },
            /*040*/ { PacketTypes.SetActiveNPC, (br) => new SetActiveNPC(br) },
            /*041*/ { PacketTypes.PlayerItemAnimation, (br) => new PlayerItemAnimation(br) },
            /*042*/ { PacketTypes.PlayerMana, (br) => new PlayerMana(br) },
            /*043*/ { PacketTypes.ManaEffect, (br) => new ManaEffect(br) },
            /*044*/ { PacketTypes.PlayerDeath, (br) => new PlayerDeath(br) },
            /*045*/ { PacketTypes.PlayerTeam, (br) => new PlayerTeam(br) },
            /*046*/ { PacketTypes.RequestSign, (br) => new RequestSign(br) },
            /*047*/ { PacketTypes.UpdateSign, (br) => new UpdateSign(br) },
            /*048*/ { PacketTypes.SetLiquid, (br) => new SetLiquid(br) },
            /*049*/ { PacketTypes.CompleteConnectionandSpawn, (br) => new CompleteConnectionandSpawn(br) },
            /*050*/ { PacketTypes.UpdatePlayerBuff, (br) => new UpdatePlayerBuff(br) },
            /*051*/ { PacketTypes.SpecialNPCEffect, (br) => new SpecialNPCEffect(br) },
            /*052*/ { PacketTypes.Unlock, (br) => new Unlock(br) },
            /*053*/ { PacketTypes.AddNPCBuff, (br) => new AddNPCBuff(br) },
            /*054*/ { PacketTypes.UpdateNPCBuff, (br) => new UpdateNPCBuff(br) },
            /*055*/ { PacketTypes.AddPlayerBuff, (br) => new AddPlayerBuff(br) },
            /*056*/ { PacketTypes.UpdateNPCName, (br) => new UpdateNPCName(br) },
            /*057*/ { PacketTypes.UpdateGoodEvil, (br) => new UpdateGoodEvil(br) },
            /*058*/ { PacketTypes.PlayMusicItem, (br) => new PlayMusicItem(br) },
            /*059*/ { PacketTypes.HitSwitch, (br) => new HitSwitch(br) },
            /*060*/ { PacketTypes.NPCHomeUpdate, (br) => new NPCHomeUpdate(br) },
            /*061*/ { PacketTypes.SpawnBossInvasion, (br) => new SpawnBossInvasion(br) },
            /*062*/ { PacketTypes.PlayerDodge, (br) => new PlayerDodge(br) },
            /*063*/ { PacketTypes.PaintTile, (br) => new PaintTile(br) },
            /*064*/ { PacketTypes.PaintWall, (br) => new PaintWall(br) },
            /*065*/ { PacketTypes.PlayerNPCTeleport, (br) => new PlayerNPCTeleport(br) },
            /*066*/ { PacketTypes.HealOtherPlayer, (br) => new HealOtherPlayer(br) },
            /*067*/ { PacketTypes.Placeholder, (br) => new Placeholder(br) },
            /*068*/ { PacketTypes.ClientUUID, (br) => new ClientUUID(br) },
            /*069*/ { PacketTypes.GetChestName, (br) => new GetChestName(br) },
            /*070*/ { PacketTypes.CatchNPC, (br) => new CatchNPC(br) },
            /*071*/ { PacketTypes.ReleaseNPC, (br) => new ReleaseNPC(br) },
            /*072*/ { PacketTypes.TravellingMerchantInventory, (br) => new TravellingMerchantInventory(br) },
            /*073*/ { PacketTypes.TeleportationPotion, (br) => new TeleportationPotion(br) },
            /*074*/ { PacketTypes.AnglerQuest, (br) => new AnglerQuest(br) },
            /*075*/ { PacketTypes.CompleteAnglerQuestToday, (br) => new CompleteAnglerQuestToday(br) },
            /*076*/ { PacketTypes.NumberOfAnglerQuestsCompleted, (br) => new NumberOfAnglerQuestsCompleted(br) },
            /*077*/ { PacketTypes.CreateTemporaryAnimation, (br) => new CreateTemporaryAnimation(br) },
            /*078*/ { PacketTypes.ReportInvasionProgress, (br) => new ReportInvasionProgress(br) },
            /*079*/ { PacketTypes.PlaceObject, (br) => new PlaceObject(br) },
            /*080*/ { PacketTypes.SyncPlayerChestIndex, (br) => new SyncPlayerChestIndex(br) },
            /*081*/ { PacketTypes.CreateCombatText, (br) => new CreateCombatText(br) },
            /*082*/ { PacketTypes.LoadNetModule, (br) => new LoadNetModule(br) },
            /*083*/ { PacketTypes.SetNPCKillCount, (br) => new SetNPCKillCount(br) },
            /*084*/ { PacketTypes.SetPlayerStealth, (br) => new SetPlayerStealth(br) },
            /*085*/ { PacketTypes.ForceItemIntoNearestChest, (br) => new ForceItemIntoNearestChest(br) },
            /*086*/ { PacketTypes.UpdateTileEntity, (br) => new UpdateTileEntity(br) },
            /*087*/ { PacketTypes.PlaceTileEntity, (br) => new PlaceTileEntity(br) },
            /*088*/ { PacketTypes.AlterItemDrop, (br) => new AlterItemDrop(br) },
            /*089*/ { PacketTypes.PlaceItemFrame, (br) => new PlaceItemFrame(br) },
            /*090*/ { PacketTypes.UpdateItemDrop2, (br) => new UpdateItemDrop2(br) },
            /*091*/ { PacketTypes.SyncEmoteBubble, (br) => new SyncEmoteBubble(br) },
            /*092*/ { PacketTypes.SyncExtraValue, (br) => new SyncExtraValue(br) },
            /*093*/ { PacketTypes.SocialHandshake, (br) => new SocialHandshake(br) },
            /*094*/ { PacketTypes.Deprecated, (br) => new Deprecated(br) },
            /*095*/ { PacketTypes.KillPortal, (br) => new KillPortal(br) },
            /*096*/ { PacketTypes.PlayerTeleportThroughPortal, (br) => new PlayerTeleportThroughPortal(br) },
            /*097*/ { PacketTypes.NotifyPlayerNPCKilled, (br) => new NotifyPlayerNPCKilled(br) },
            /*098*/ { PacketTypes.NotifyPlayerOfEvent, (br) => new NotifyPlayerOfEvent(br) },
            /*099*/ { PacketTypes.UpdateMinionTarget, (br) => new UpdateMinionTarget(br) },
            /*100*/ { PacketTypes.NPCTeleportThroughPortal, (br) => new NPCTeleportThroughPortal(br) },
            /*101*/ { PacketTypes.UpdateShieldStrengths, (br) => new UpdateShieldStrengths(br) },
            /*102*/ { PacketTypes.NebulaLevelUpRequest, (br) => new NebulaLevelUpRequest(br) },
            /*103*/ { PacketTypes.UpdateMoonLordCountdown, (br) => new UpdateMoonLordCountdown(br) },
            /*104*/ { PacketTypes.SetNPCShopItem, (br) => new SetNPCShopItem(br) },
            /*105*/ { PacketTypes.ToggleGemLock, (br) => new ToggleGemLock(br) },
            /*106*/ { PacketTypes.PoofofSmoke, (br) => new PoofofSmoke(br) },
            /*107*/ { PacketTypes.ChatMessagev2, (br) => new ChatMessagev2(br) },
            /*108*/ { PacketTypes.WiredCannonShot, (br) => new WiredCannonShot(br) },
            /*109*/ { PacketTypes.MassWireOperation, (br) => new MassWireOperation(br) },
            /*110*/ { PacketTypes.MassWireOperationConsume, (br) => new MassWireOperationConsume(br) },
            /*111*/ { PacketTypes.ToggleBirthdayParty, (br) => new ToggleBirthdayParty(br) },
            /*112*/ { PacketTypes.GrowFX, (br) => new GrowFX(br) },
            /*113*/ { PacketTypes.CrystalInvasionStart, (br) => new CrystalInvasionStart(br) },
            /*114*/ { PacketTypes.CrystalInvasionWipeAll, (br) => new CrystalInvasionWipeAll(br) },
            /*115*/ { PacketTypes.MinionAttackTargetUpdate, (br) => new MinionAttackTargetUpdate(br) },
            /*116*/ { PacketTypes.CrystalInvasionSendWaitTime, (br) => new CrystalInvasionSendWaitTime(br) },
            /*117*/ { PacketTypes.PlayerHurtV2, (br) => new PlayerHurtV2(br) },
            /*118*/ { PacketTypes.PlayerDeathV2, (br) => new PlayerDeathV2(br) }
        };

        /// <summary>
        /// Gets or sets the Packet ID.
        /// </summary>
        public byte ID { get; protected set; }

        /// <summary>
        /// Gets the type of the packet.
        /// </summary>
        public PacketTypes PacketType
        {
            get
            {
                return (PacketTypes)this.ID;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TerrariaPacket"/> with 
        /// the specified BinaryReader object to deserialize a derivative on.
        /// </summary>
        /// <param name="br">
        /// A reference to a BinaryReader which contains binary payload to be deserialized into
        /// a fully-qualified TerrariaPacket.
        /// </param>
        protected TerrariaPacket(BinaryReader br)
        {
            _length = br.ReadInt16();
            this.ID = br.ReadByte();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TerrariaPacket"/> class.
        /// </summary>
        /// <param name="id">Identifier.</param>
        protected TerrariaPacket(byte id)
        {
            this.ID = id;
        }

        /// <summary>
        /// Deserializes a packet from the specified binary reader and returns a TerrariaPacket 
        /// derivative according to the deserializer methods in deserializerMap.
        /// </summary>
        /// <param name="br">
        /// An instance of a BinaryReader which contains a binary terraria packet payload in 
        /// which to deserialize an object from
        /// </param>
        /// <param name="id">
        /// Packet identifier that is used to find the deserializer method via deserializerMap
        /// </param>
        public static TerrariaPacket Deserialize(BinaryReader br, byte id)
        {
            br.BaseStream.Seek(0, SeekOrigin.Begin);

            if (deserializerMap.ContainsKey((PacketTypes)id) == false)
            {
                return new UnknownPacket(br);
            }

            return deserializerMap[(PacketTypes)id](br);
        }

        /// <summary>
        /// Deserializes a packet from the specified binary reader and returns a TerrariaPacket 
        /// derivative according to the deserializer methods in deserializerMap.
        /// </summary>
        /// <param name="br">
        /// An instance of a BinaryReader which contains a binary terraria packet payload in 
        /// which to deserialize an object from
        /// </param>
        public static TerrariaPacket Deserialize(BinaryReader br)
        {
            br.ReadInt16();
            byte id = br.ReadByte();

            return Deserialize(br, id);
        }

		/// <summary>
		/// Serializes this TerrariaNetworkObject instance into the provided stream.
		/// </summary>
		/// <param name="stream">
		/// A reference to a valid, open, and writable stream object in which to serialize this
		/// instance to.
		/// </param>
		public override void ToStream(Stream stream, bool includeHeader = true)
		{
			if (includeHeader == false)
			{
				return;
			}

			using (BinaryWriter br = new BinaryWriter(stream, System.Text.Encoding.UTF8, leaveOpen: true))
			{
				br.Write((short)(GetLength() + PACKET_HEADER_LEN));
				br.Write(ID);
			}
		}
	}
}

