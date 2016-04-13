using System.Collections.Generic;
using UCS.Helpers;

namespace UCS.PacketProcessing
{
    //Packet 20113
    internal class SetDeviceTokenMessage : Message
    {
        public SetDeviceTokenMessage(Client client) : base(client)
        {
            SetMessageType(20113);
        }

        public string UserToken { get; set; }

        public override void Encode()
        {
            var pack = new List<byte>();
            pack.AddString(UserToken);
            Encrypt(pack.ToArray());
        }
    }
}