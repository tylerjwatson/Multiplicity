using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplicity.Packets
{
	public abstract class TerrariaNetModule : TerrariaNetworkObject
	{
		/// <summary>
		/// The deserializer map.
		/// 
		/// Deserializer maps point to a function to return a fully qualified packet
		/// from one supplied BinaryReader object.  Derivatives of TerrariaPacket
		/// should make sure that they return a valid packet structure when passed a
		/// BinaryReader to deserialize from.
		/// </summary>
		public static Dictionary<NetworkModuleTypes, Func<BinaryReader, TerrariaNetModule>> deserializerMap =
			new Dictionary<NetworkModuleTypes, Func<BinaryReader, TerrariaNetModule>>()
			{
				{ NetworkModuleTypes.NetLiquidModule, (br) => new NetLiquidModule(br) },
				{ NetworkModuleTypes.NetTextModule, (br) => new NetTextModule(br) }
			};

		/// <summary>
		/// Gets or sets the NetModule ID.
		/// </summary>
		public int ID { get; protected set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="TerrariaPacket"/> with 
		/// the specified BinaryReader object to deserialize a derivative on.
		/// </summary>
		/// <param name="br">
		/// A reference to a BinaryReader which contains binary payload to be deserialized into
		/// a fully-qualified TerrariaPacket.
		/// </param>
		protected TerrariaNetModule(BinaryReader br)
		{
			this.ID = (int)br.ReadUInt16();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TerrariaPacket"/> class.
		/// </summary>
		/// <param name="id">Identifier.</param>
		protected TerrariaNetModule(byte id)
		{
			this.ID = id;
		}

		/// <summary>
		/// Deserializes a net module from the specified binary reader and returns a TerrariaNetModule 
		/// derivative according to the deserializer methods in deserializerMap.
		/// </summary>
		/// <param name="br">
		/// An instance of a BinaryReader which contains a binary net module payload in 
		/// which to deserialize an object from
		/// </param>
		/// <param name="id">
		/// Packet identifier that is used to find the deserializer method via deserializerMap
		/// </param>
		public static TerrariaNetModule Deserialize(BinaryReader br)
		{
			br.BaseStream.Seek(0, SeekOrigin.Begin);

			int id = br.ReadUInt16();

			if (deserializerMap.ContainsKey((NetworkModuleTypes)id) == false)
			{
				return new UnknownNetModule(br);
			}

			return deserializerMap[(NetworkModuleTypes)id](br);
		}

		public override void ToStream(Stream stream, bool includeHeader = true)
		{
			if (includeHeader == false)
			{
				return;
			}

			using (BinaryWriter br = new BinaryWriter(stream, System.Text.Encoding.UTF8, leaveOpen: true))
			{
				br.Write(ID);
			}
		}
	}
}
