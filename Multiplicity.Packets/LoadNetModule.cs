using System.IO;

namespace Multiplicity.Packets
{
    /// <summary>
    /// The LoadNetModule (0x52) packet.
    /// </summary>
    public class LoadNetModule : TerrariaPacket
    {
		public TerrariaNetModule LoadedModule { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadNetModule"/> class.
        /// </summary>
        public LoadNetModule()
            : base((byte)PacketTypes.LoadNetModule)
        {
			
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadNetModule"/> class.
        /// </summary>
        /// <param name="br">br</param>
        public LoadNetModule(BinaryReader br)
            : base(br)
        {
			LoadedModule = TerrariaNetModule.Deserialize(br);
		}

        public override string ToString()
        {
            return string.Format("[LoadNetModule]");
        }

        #region implemented abstract members of TerrariaPacket

        public override short GetLength()
        {
            return (short)(0);
        }

        public override void ToStream(Stream stream, bool includeHeader = true)
        {
            /*
             * Length and ID headers get written in the base packet class.
             */
            if (includeHeader) {
                base.ToStream(stream, includeHeader);
            }

			/*
             * Always make sure to not close the stream when serializing.
             * 
             * It is up to the caller to decide if the underlying stream
             * gets closed.  If this is a network stream we do not want
             * the regressions of unconditionally closing the TCP socket
             * once the payload of data has been sent to the client.
             */
			LoadedModule.ToStream(stream, includeHeader);
        }

        #endregion

    }
}
