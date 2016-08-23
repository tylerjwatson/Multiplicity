using System.Drawing;
using System.IO;

namespace Multiplicity.Packets.Extensions
{
    public static class BinaryReaderExtensions
    {
        public static Color ReadColor(this BinaryReader br)
        {
            byte[] colourPayload = br.ReadBytes(3);
            return Color.FromArgb(colourPayload[0], colourPayload[1], colourPayload[2]);
        }
    }
}

