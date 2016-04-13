using System.Collections.Generic;

namespace UCS.PacketProcessing
{
    internal class BookmarkRemoveAllianceMessage : Message
    {
        public BookmarkRemoveAllianceMessage(Client client) : base(client)
        {
            SetMessageType(24344);
        }

        public override void Encode()
        {
            var data = new List<byte>();
            Encrypt(data.ToArray());
        }
    }
}