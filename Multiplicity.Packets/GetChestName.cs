//
//  ChestName.cs
//
//  Author:
//       Luke S <rt.luke.s@gmail.com>
//
//  Copyright (c) 2016 Luke S

using System.IO;

namespace Multiplicity.Packets
{
    public class GetChestName : TerrariaPacket
    {
        public short ChestID { get; set; }
        public short X { get; set; }
        public short Y { get; set; }
        public string Name { get; set; }

        public GetChestName(short chestID, short x, short y, string name)
            : base((byte)PacketTypes.GetChestName)
        {
            this.ChestID = chestID;
            this.X = x;
            this.Y = y;
            this.Name = name;
        }

        public GetChestName(BinaryReader br)
            : base(br)
        {
            this.ChestID = br.ReadInt16();
            this.X = br.ReadInt16();
            this.Y = br.ReadInt16();
            this.Name = br.ReadString();
        }

        public override void ToStream(Stream stream, bool includeHeader = true)
        {
            base.ToStream(stream, includeHeader);

            using (BinaryWriter bw = new BinaryWriter(stream, System.Text.Encoding.UTF8, leaveOpen: true))
            {
                bw.Write(this.ChestID);
                bw.Write(this.X);
                bw.Write(this.Y);
                bw.Write(this.Name);
            }
        }

        public override short GetLength()
        {
            return (short)(6 + this.Name.Length);
        }

        public override string ToString() =>
            $"[{nameof(GetChestName)}: ChestID={ChestID},X={X},Y={Y},Name={Name}]";
    }
}
