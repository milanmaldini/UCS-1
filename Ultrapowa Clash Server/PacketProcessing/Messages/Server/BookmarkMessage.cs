using System.Collections.Generic;
using UCS.Helpers;

namespace UCS.PacketProcessing
{
    internal class BookmarkMessage : Message
    {
        public BookmarkMessage(Client client) : base(client)
        {
            SetMessageType(24340);
        }

        public override void Encode()
        {
            var data = new List<byte>();
            data.AddInt64(1);
            data.AddInt64(2);
            Encrypt(data.ToArray());
        }
    }
}