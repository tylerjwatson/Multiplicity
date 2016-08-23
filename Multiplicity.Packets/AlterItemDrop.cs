//
//  AlterItemDrop.cs
//
//  Author:
//       Josh Harris <celant@celantinteractive.com>
//
//  Copyright (c) 2016 Celant

using System.IO;

namespace Multiplicity.Packets
{
    public class AlterItemDrop : TerrariaPacket
    {
        public short ItemIndex { get; set; }

        public byte Flags1 { get; set; }

        public uint PackedColorValue { get; set; }

        public ushort Damage { get; set; }

        public float Knockback { get; set; }

        public ushort UseAnimation { get; set; }

        public ushort UseTime { get; set; }

        public short Shoot { get; set; }

        public float ShootSpeed { get; set; }

        public byte Flags2 { get; set; }

        public short Width { get; set; }

        public short Height { get; set; }

        public float Scale { get; set; }

        public short Ammo { get; set; }

        public short UseAmmo { get; set; }

        public bool NotAmmo { get; set; }

        public AlterItemDrop()
            : base((byte)PacketTypes.AlterItemDrop)
        {
        }

        public AlterItemDrop(BinaryReader br)
            : base(br)
        {
            this.ItemIndex = br.ReadInt16();
            this.Flags1 = br.ReadByte();
            this.PackedColorValue = br.ReadUInt32();
            this.Damage = br.ReadUInt16();
            this.Knockback = br.ReadSingle();
            this.UseAnimation = br.ReadUInt16();
            this.UseTime = br.ReadUInt16();
            this.Shoot = br.ReadInt16();
            this.ShootSpeed = br.ReadSingle();
            this.Flags2 = br.ReadByte();
            this.Width = br.ReadInt16();
            this.Height = br.ReadInt16();
            this.Scale = br.ReadSingle();
            this.Ammo = br.ReadInt16();
            this.UseAmmo = br.ReadInt16();
            this.NotAmmo = br.ReadBoolean();
        }

        public override short GetLength()
        {
            return 37;
        }

        public override void ToStream(Stream stream, bool includeHeader = true)
        {
            base.ToStream(stream, includeHeader);

            using (BinaryWriter bw = new BinaryWriter(stream, System.Text.Encoding.UTF8, leaveOpen: true))
            {
                bw.Write(ItemIndex);
                bw.Write(Flags1);
                bw.Write(PackedColorValue);
                bw.Write(Damage);
                bw.Write(Knockback);
                bw.Write(UseAnimation);
                bw.Write(UseTime);
                bw.Write(Shoot);
                bw.Write(ShootSpeed);
                bw.Write(Flags2);
                bw.Write(Width);
                bw.Write(Height);
                bw.Write(Scale);
                bw.Write(Ammo);
                bw.Write(UseAmmo);
                bw.Write(NotAmmo);
            }
        }

        public override string ToString()
        {
            return $"[AlterItemDrop ItemIndex: {ItemIndex}, Width: {Width}, Height: {Height}]";
        }
    }
}

