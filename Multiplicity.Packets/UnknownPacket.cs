using System;
using System.IO;

namespace Multiplicity.Packets
{
	public class UnknownPacket : TerrariaPacket
	{
		public byte[] payload { get; set; }

		public UnknownPacket(BinaryReader br)
			: base(br)
		{
			payload = br.ReadBytes(_length - TerrariaPacket.PACKET_HEADER_LEN);
		}

        public override short GetLength()
		{
			return (short)(_length - TerrariaPacket.PACKET_HEADER_LEN);
		}

		public override void ToStream(Stream stream, bool includeHeader = true)
		{
			base.ToStream(stream, includeHeader);

			using (BinaryWriter bw = new BinaryWriter(stream, System.Text.Encoding.UTF8, leaveOpen: true)) {
				bw.Write(payload);
			}
		}

		public override string ToString()
		{
			string hex = BitConverter.ToString(payload).Replace("-", string.Empty);

			return $"[UnknownPacket: ID={ID} Len={GetLength()} Content={hex}]";
		}
	}
}

