using System;
using System.IO;

namespace Multiplicity.Packets
{
	public class UnknownNetModule : TerrariaNetModule
	{
		public UnknownNetModule(BinaryReader br) : base(br)
		{
		}

		public override short GetLength()
		{
			throw new InvalidOperationException("Cannot determine length of unknown net module type.");
		}
	}
}