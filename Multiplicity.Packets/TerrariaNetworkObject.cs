using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplicity.Packets
{
	public abstract class TerrariaNetworkObject
	{
		/// <summary>
		/// Gets the object length in bytes.
		/// </summary>
		public abstract short GetLength();

		/// <summary>
		/// Gets or sets the CRC32 hash for this TerrariaNetworkObject.
		/// </summary>
		/// <value>The CRC.</value>
		public uint CRC { get; internal set; }

		/// <summary>
		/// Serializes this TerrariaNetworkObject instance into the provided stream.
		/// </summary>
		/// <param name="stream">
		/// A reference to a valid, open, and writable stream object in which to serialize this
		/// instance to.
		/// </param>
		public abstract void ToStream(Stream stream, bool includeHeader = true);

		/// <summary>
		/// Returns a byte array with the binary contents of this TerrariaPacket instance.
		/// </summary>
		public virtual byte[] ToArray(bool includeHeader = true)
		{
			using (MemoryStream ms = new MemoryStream())
			{
				ToStream(ms, includeHeader);
				return ms.ToArray();
			}
		}
	}
}
