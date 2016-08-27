using System.IO;

namespace Multiplicity.Packets
{
	/// <summary>
	/// The ProjectileUpdate (27) packet.
	/// </summary>
	public class ProjectileUpdate : TerrariaPacket
	{
		//Always sent
		public short ProjectileID { get; set; }
		public float PositionX { get; set; }
		public float PositionY { get; set; }
		public float VelocityX { get; set; }
		public float VelocityY { get; set; }
		public float KnockBack { get; set; }
		public short Damage { get; set; }
		public byte Owner { get; set; }
		public short Type { get; set; }
		public AIFlags Flags { get; set; }

		//Sent conditionally
		public float[] AI { get; set; }
		public short UUID { get; set; }

		public const int MaxAI = 2;

		public bool HasAI0
		{
			get
			{
				return (this.Flags & AIFlags.AI0) != 0;
			}
			set
			{
				if (value)
				{
					this.Flags |= AIFlags.AI0;
				}
				else
				{
					this.Flags &= ~AIFlags.AI0;
				}
			}
		}

		public bool HasAI1
		{
			get
			{
				return (this.Flags & AIFlags.AI1) != 0;
			}
			set
			{
				if (value)
				{
					this.Flags |= AIFlags.AI1;
				}
				else
				{
					this.Flags &= ~AIFlags.AI1;
				}
			}
		}

		public bool HasUUID
		{
			get
			{
				return (this.Flags & AIFlags.HasUUID) != 0;
			}
			set
			{
				if (value)
				{
					this.Flags |= AIFlags.HasUUID;
				}
				else
				{
					this.Flags &= ~AIFlags.HasUUID;
				}
			}
		}

		[System.Flags]
		public enum AIFlags : byte
		{
			AI0 = 1,
			AI1 = 2,
			HasUUID = 4
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ProjectileUpdate"/> class.
		/// </summary>
		public ProjectileUpdate()
			: base((byte)PacketTypes.ProjectileUpdate)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ProjectileUpdate"/> class.
		/// </summary>
		/// <param name="br">br</param>
		public ProjectileUpdate(BinaryReader br)
			: base(br)
		{
			this.ProjectileID = br.ReadInt16();
			this.PositionX = br.ReadSingle();
			this.PositionY = br.ReadSingle();
			this.VelocityX = br.ReadSingle();
			this.VelocityY = br.ReadSingle();
			this.KnockBack = br.ReadSingle();
			this.Damage = br.ReadInt16();
			this.Owner = br.ReadByte();
			this.Type = br.ReadInt16();
			this.Flags = (AIFlags)br.ReadByte();

			this.AI = new float[MaxAI];
			for (var i = 0; i < MaxAI; i++)
			{
				if (((byte)this.Flags & (1 << i)) != 0)
				{
					this.AI[i] = br.ReadSingle();
				}
				else
				{
					this.AI[i] = 0f;
				}
			}

			if (HasUUID)
			{
				this.UUID = br.ReadInt16();
			}
		}

		public override string ToString()
		{
			return $"[{nameof(ProjectileUpdate)}: ProjectileID={ProjectileID},PositionX={PositionX}," +
				$"PositionY={PositionY},VelocityX={VelocityX},VelocityY={VelocityY},KnockBack={KnockBack}," +
				$"Damage={Damage},Owner={Owner},Type={Type},Flags={Flags},UUID={UUID}]";
		}

		#region implemented abstract members of TerrariaPacket

		public override short GetLength()
		{
			short length = 28;

			if (HasAI0)
				length += 4;
			if (HasAI1)
				length += 4;
			if (HasUUID)
				length += 2;

			return length;
		}

		public override void ToStream(Stream stream, bool includeHeader = true)
		{
			/*
             * Length and ID headers get written in the base packet class.
             */
			if (includeHeader)
			{
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
			using (BinaryWriter writer = new BinaryWriter(stream, new System.Text.UTF8Encoding(), leaveOpen: true))
			{
				writer.Write(ProjectileID);
				writer.Write(PositionX);
				writer.Write(PositionY);
				writer.Write(VelocityX);
				writer.Write(VelocityY);
				writer.Write(KnockBack);
				writer.Write(Damage);
				writer.Write(Owner);
				writer.Write(Type);
				writer.Write((byte)Flags);

				for (var i = 0; i < MaxAI; i++)
				{
					if (((byte)this.Flags & (1 << i)) != 0)
					{
						writer.Write(this.AI[i]);
					}
				}

				if (HasUUID)
				{
					writer.Write(this.UUID);
				}
			}
		}

		#endregion
	}
}