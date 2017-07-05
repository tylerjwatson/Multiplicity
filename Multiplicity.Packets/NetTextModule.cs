using System;
using System.IO;

namespace Multiplicity.Packets
{
	/// <summary>
	/// NetModule used for most chat and message communications.
	/// </summary>
	public class NetTextModule : TerrariaNetModule
	{
		/// <summary>
		/// The command this message may have executed. Includes things such as "Party" or "Say".
		/// </summary>
		public string ChatCommand { get; set; }

		/// <summary>
		/// The message send with this chat.
		/// </summary>
		public string ChatMessage { get; set; }

		public NetTextModule(BinaryReader br) : base(br)
		{
			ChatCommand = br.ReadString();
			ChatMessage = br.ReadString();
		}

		public override short GetLength()
		{
			return (short)(2 + ChatCommand.Length + ChatMessage.Length);
		}

		public override void ToStream(Stream stream, bool includeHeader = true)
		{
			if (includeHeader)
			{
				base.ToStream(stream, includeHeader);
			}

			using (BinaryWriter br = new BinaryWriter(stream, new System.Text.UTF8Encoding(), leaveOpen: true))
			{
				br.Write(ChatCommand);
				br.Write(ChatMessage);
			}
		}
	}
}