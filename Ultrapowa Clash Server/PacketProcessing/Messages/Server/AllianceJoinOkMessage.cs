using System.Collections.Generic;

namespace UCS.PacketProcessing
{
    //Packet 24303
    internal class AllianceJoinOkMessage : Message
    {
        public AllianceJoinOkMessage(Client client) : base(client)
        {
            SetMessageType(24303);
        }

        public override void Encode()
        {
            var pack = new List<byte>();
            Encrypt(pack.ToArray());
        }
    }
}