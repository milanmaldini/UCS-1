using System.Collections.Generic;
using UCS.Helpers;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    //Packet 20105
    internal class FriendListMessage : Message
    {
        public FriendListMessage(Client client) : base(client)
        {
            SetMessageType(20105);
        }

        public override void Encode()
        {
            var pack = new List<byte>();
            pack.AddInt32(0);
            pack.AddInt32(0);
            pack.AddDataSlots(new List<DataSlot>());
            Encrypt(pack.ToArray());
        }
    }
}