﻿namespace Multiplicity.Packets
{
	public enum PacketTypes : byte
	{
		/*001*/ ConnectRequest = 1,
		/*002*/ Disconnect,
		/*003*/ ContinueConnecting,
		/*004*/ PlayerInfo,
		/*005*/ PlayerInventorySlot,
		/*006*/ ContinueConnecting2,
		/*007*/ WorldInfo,
		/*008*/ GetSection,
		/*009*/ Status,
		/*010*/ SendSection,
		/*011*/ SectionTileFrame,
		/*012*/ SpawnPlayer,
		/*013*/ UpdatePlayer,
		/*014*/ PlayerActive,
		/*015*/ Null,
		/*016*/ PlayerHP,
		/*017*/ ModifyTile,
		/*018*/ Time,
		/*019*/ DoorToggle,
		/*020*/ SendTileSquare,
		/*021*/ UpdateItemDrop,
		/*022*/ UpdateItemOwner,
		/*023*/ NPCUpdate,
		/*024*/ StrikeNPCwithHeldItem,
		/*025*/ ChatMessage,
		/*026*/ PlayerDamage,
		/*027*/ ProjectileUpdate,
		/*028*/ NPCStrike,
		/*029*/ DestroyProjectile,
		/*030*/ TogglePVP,
		/*031*/ GetChestContents,
		/*032*/ ChestItem,
		/*033*/ SetChestName,
		/*034*/ PlaceChest,
		/*035*/ HealEffect,
		/*036*/ PlayerZone,
		/*037*/ RequestPassword,
		/*038*/ SendPassword,
		/*039*/ RemoveItemOwner,
		/*040*/ SetActiveNPC,
		/*041*/ PlayerItemAnimation,
		/*042*/ PlayerMana,
		/*043*/ ManaEffect,
		/*044*/ PlayerDeath,
		/*045*/ PlayerTeam,
		/*046*/ RequestSign,
		/*047*/ UpdateSign,
		/*048*/ SetLiquid,
		/*049*/ CompleteConnectionandSpawn,
		/*050*/ UpdatePlayerBuff,
		/*051*/ SpecialNPCEffect,
		/*052*/ Unlock,
		/*053*/ AddNPCBuff,
		/*054*/ UpdateNPCBuff,
		/*055*/ AddPlayerBuff,
		/*056*/ UpdateNPCName,
		/*057*/ UpdateGoodEvil,
		/*058*/ PlayMusicItem,
		/*059*/ HitSwitch,
		/*060*/ NPCHomeUpdate,
		/*061*/ SpawnBossInvasion,
		/*062*/ PlayerDodge,
		/*063*/ PaintTile,
		/*064*/ PaintWall,
		/*065*/ PlayerNPCTeleport,
		/*066*/ HealOtherPlayer,
		/*067*/ Placeholder,
		/*068*/ ClientUUID,
		/*069*/ GetChestName,
		/*070*/ CatchNPC,
		/*071*/ ReleaseNPC,
		/*072*/ TravellingMerchantInventory,
		/*073*/ TeleportationPotion,
		/*074*/ AnglerQuest,
		/*075*/ CompleteAnglerQuestToday,
		/*076*/ NumberOfAnglerQuestsCompleted,
		/*077*/ CreateTemporaryAnimation,
		/*078*/ ReportInvasionProgress,
		/*079*/ PlaceObject,
		/*080*/ SyncPlayerChestIndex,
		/*081*/ CreateCombatText,
		/*082*/ LoadNetModule,
		/*083*/ SetNPCKillCount,
		/*084*/ SetPlayerStealth,
		/*085*/ ForceItemIntoNearestChest,
		/*086*/ UpdateTileEntity,
		/*087*/ PlaceTileEntity,
		/*088*/ AlterItemDrop,
		/*089*/ PlaceItemFrame,
		/*090*/ UpdateItemDrop2,
		/*091*/ SyncEmoteBubble,
		/*092*/ SyncExtraValue,
		/*093*/ SocialHandshake,
		/*094*/ Deprecated,
		/*095*/ KillPortal,
		/*096*/ PlayerTeleportThroughPortal,
		/*097*/ NotifyPlayerNPCKilled,
		/*098*/ NotifyPlayerOfEvent,
		/*099*/ UpdateMinionTarget,
		/*100*/ NPCTeleportThroughPortal,
		/*101*/ UpdateShieldStrengths,
		/*102*/ NebulaLevelUpRequest,
		/*103*/ UpdateMoonLordCountdown,
		/*104*/ SetNPCShopItem,
		/*105*/ ToggleGemLock,
		/*106*/ PoofofSmoke,
		/*107*/ ChatMessagev2,
		/*108*/ WiredCannonShot,
		/*109*/ MassWireOperation,
		/*110*/ MassWireOperationConsume,
		/*111*/ ToggleBirthdayParty,
		/*112*/ GrowFX
    }
}

