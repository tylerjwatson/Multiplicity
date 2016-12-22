using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplicity.Packets.Extensions
{
    public static class ByteExtensions
    {
        public static byte SetBit(this byte b, int bit, bool value)
        {
            if (value)
            {
                return b = (byte)(b | (1 << bit));
            }
            else
            {
                return b = (byte)(b & ~(1 << bit));
            }
        }

        public static bool ReadBit(this byte b, int bit)
        {
            return (b & (1 << bit)) != 0;
        }
    }
}
