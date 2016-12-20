using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplicity.Packets.Extensions
{
    public static class ByteExtensions
    {
        public static bool ReadBit(this byte b, int bit)
        {
            return (b & (1 << bit)) != 0;
        }
    }
}
