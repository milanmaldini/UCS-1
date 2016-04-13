using System.Collections.Generic;

namespace UCS.PacketProcessing
{
    //Packet 20108
    internal class KeepAliveOkMessage : Message
    {
        public KeepAliveOkMessage(Client client, KeepAliveMessage cka) : base(client)
        {
            SetMessageType(20108);
        }

        public override void Encode()
        {
            var data = new List<byte>();
            var packet = data.ToArray();
            Encrypt(packet);
        }
    }
}