using System;
using System.IO;

namespace Multiplicity.Packets
{
	[Flags]
	public enum PlayerControlFlags : byte
	{
		None = 0,
		Up = 1,
		Down = 1 << 1,
		Left = 1 << 2,
		Right = 1 << 3,
		Jump = 1 << 4,
		UseItem = 1 << 5,
		Direction = 1 << 6
	}

	[Flags]
	public enum PulleyDirectionFlags : byte
	{
		None = 0,
		Direction1 = 1,
		Direction2 = 1 << 1
	}

	public class UpdatePlayer : TerrariaPacket
	{
		public byte PlayerID { get; protected set; }
		public PlayerControlFlags Control { get; set; }
		public byte SelectedItem { get; set; }
		public float PositionX { get; set; }
		public float PositionY { get; set; }
		public float VelocityX { get; set; }
		public float VelocityY { get; set; }
		public PulleyDirectionFlags Pulley { get; set; }

		public UpdatePlayer()
			: base((byte)PacketTypes.UpdatePlayer)
		{

		}

		public UpdatePlayer(BinaryReader br)
			: base(br)
		{
			PlayerID = br.ReadByte();
			Control = (PlayerControlFlags)br.ReadByte();
			SelectedItem = br.ReadByte();
			PositionX = br.ReadSingle();
			PositionY = br.ReadSingle();
			VelocityX = br.ReadSingle();
			VelocityY = br.ReadSingle();
			Pulley = (PulleyDirectionFlags)br.ReadByte();
		}

		public override short GetLength()
		{
			return 20;
		}

		public override void ToStream(Stream stream, bool includeHeader = true)
		{
			base.ToStream(stream, includeHeader);

			using (BinaryWriter bw = new BinaryWriter(stream, System.Text.Encoding.UTF8, leaveOpen: true))
			{
				bw.Write(PlayerID);
				bw.Write((byte)Control);
				bw.Write(SelectedItem);
				bw.Write(PositionX);
				bw.Write(PositionY);
				bw.Write(VelocityX);
				bw.Write(VelocityY);
				bw.Write((byte)Pulley);
			}
		}

		public override string ToString()
		{
			return
				$"[UpdatePlayer: PlayerID={PlayerID}, Control={Control}, SelectedItem={SelectedItem}, PositionX={PositionX}, PositionY={PositionY}, VelocityX={VelocityX}, VelocityY={VelocityY}, Pulley={Pulley}]";
		}
	}
}

