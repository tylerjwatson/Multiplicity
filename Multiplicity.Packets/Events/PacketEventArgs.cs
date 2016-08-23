using System;

namespace Multiplicity.Packets.Events
{
	/// <summary>
	/// Packet event arguments.
	/// </summary>
	public class PacketEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the packet received by the client.
		/// </summary>
		public TerrariaPacket Packet { get; set; }
	}
}

