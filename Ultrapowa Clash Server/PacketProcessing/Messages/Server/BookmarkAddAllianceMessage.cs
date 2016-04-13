using System.Collections.Generic;

namespace UCS.PacketProcessing
{
    internal class BookmarkAddAllianceMessage : Message
    {
        public BookmarkAddAllianceMessage(Client client) : base(client)
        {
            SetMessageType(24343);
        }

        public override void Encode()
        {
            var data = new List<byte>();
            Encrypt(data.ToArray());
        }
    }
}