using System;
using System.IO;

namespace Multiplicity.Packets
{
    [Flags]
    public enum NPCUpdateFlags : byte
    {
        None = 0,
        DirectionX = 1,
        DirectionY = 1 << 1,
        AI3 = 1 << 2,
        AI2 = 1 << 3,
        AI1 = 1 << 4,
        AI0 = 1 << 5,
        SpriteDirection = 1 << 6,
        FullLife = 1 << 7
    }

    public class NPCUpdate : TerrariaPacket
    {
        protected short _npcLifeBytes = 1;
        protected bool _releaseOwner = false;

        public short NPCID { get; protected set; }

        public float PositionX { get; set; }

        public float PositionY { get; set; }

        public float VelocityX { get; set; }

        public float VelocityY { get; set; }

        public byte Target { get; set; }

        public NPCUpdateFlags Flags { get; set; }

        public int? Life { get; set; }

        public float[] AI { get; set; }

        public short NPCNetID { get; set; }

        public byte ReleaseOwner { get; set; }

        public NPCUpdate()
            : base((byte)PacketTypes.NPCUpdate)
        {
            this.NPCID = NPCID;
            this.AI = new float[4];
        }

        public NPCUpdate(BinaryReader br)
            : base(br)
        {
            this.NPCID = br.ReadInt16();
            this.PositionX = br.ReadSingle();
            this.PositionY = br.ReadSingle();
            this.VelocityX = br.ReadSingle();
            this.VelocityY = br.ReadSingle();
            this.Target = br.ReadByte();
            this.Flags = (NPCUpdateFlags)br.ReadByte();

            this.AI = new float[4];

            for (int i = 0; i < 4; i++)
            {
                float ai = 0;

                if (((byte)Flags & (1 << (i + 2))) != 0)
                {
                    ai = br.ReadSingle();
                }

                AI[i] = ai;
            }

            this.NPCNetID = br.ReadInt16();

            if ((Flags & NPCUpdateFlags.FullLife) == NPCUpdateFlags.None)
            {
                /*
                 * This is a fucking filthy hack, have to take stream length
                 * as a way to work out how much packet buffer we have left
                 * because the Terraria process has a runtime dictionary of NPC
                 * life bytes which tells the packet processor how many bytes of
                 * the NPC life there is in the packet.
                 * 
                 * We don't have access to this information short of blurting
                 * up our on dictionary of NPC life bytes in which I would rather
                 * kill myself than do.
                 */
                long bufferLeft = br.BaseStream.Length - br.BaseStream.Position;

                if (bufferLeft >= 4)
                {
                    this.Life = (int)br.ReadInt32();
                    _npcLifeBytes = 4;
                }
                else if (bufferLeft >= 2)
                {
                    this.Life = (int)br.ReadInt16();
                    _npcLifeBytes = 2;
                }
                else
                {
                    this.Life = (int)br.ReadSByte();
                    _npcLifeBytes = 1;
                }
            }

            if (br.BaseStream.Length - br.BaseStream.Position > 0)
            {
                _releaseOwner = true;
                this.ReleaseOwner = br.ReadByte();
            }
        }

        public override short GetLength()
        {
            short fixedLen = 22;

            /*
			 * Dynamic packet sizes fucking suck balls
			 */

            for (int i = 0; i < 4; i++)
            {
                if (((byte)Flags & (1 << (i + 2))) != 0)
                {
                    fixedLen += 4;
                }
            }


            if ((Flags & NPCUpdateFlags.FullLife) == NPCUpdateFlags.None)
            {
                fixedLen += _npcLifeBytes;
            }

            if (_releaseOwner)
            {
                fixedLen += 1;
            }

            return fixedLen;
        }

        public override void ToStream(Stream stream, bool includeHeader = true)
        {
            base.ToStream(stream, includeHeader);

            using (BinaryWriter bw = new BinaryWriter(stream, System.Text.Encoding.UTF8, leaveOpen: true))
            {
                bw.Write(this.NPCID);
                bw.Write(this.PositionX);
                bw.Write(this.PositionY);
                bw.Write(this.VelocityX);
                bw.Write(this.VelocityY);
                bw.Write(this.Target);
                bw.Write((byte)this.Flags);

                for (int i = 0; i < 4; i++)
                {
                    if (((byte)Flags & (1 << (i + 2))) != 0)
                    {
                        bw.Write(AI[i]);
                    }
                }

                bw.Write(NPCNetID);

                if ((Flags & NPCUpdateFlags.FullLife) == NPCUpdateFlags.None)
                {
                    switch (_npcLifeBytes)
                    {
                        case 4:
                            bw.Write(Life.Value);
                            break;
                        case 2:
                            bw.Write((short)Life.Value);
                            break;
                        case 1:
                            bw.Write((sbyte)Life.Value);
                            break;
                    }
                }

                if (_releaseOwner)
                {
                    bw.Write(ReleaseOwner);
                }
            }
        }

        public override string ToString()
        {
            return string.Format("[NPCUpdate: NPCID={0}, PositionX={1}, PositionY={2}, VelocityX={3}, VelocityY={4}, Target={5}, Flags={6}, Life={7}, AI={8}, NPCNetID={9}, ReleaseOwner={10}]",
                NPCID, PositionX, PositionY, VelocityX, VelocityY, Target, Flags, Life, AI, NPCNetID, ReleaseOwner);
        }
    }
}

