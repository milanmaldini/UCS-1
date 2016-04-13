using System.IO;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    internal class GetDeviceTokenMessage : Message
    {
        public int Unknown1;
        public string UserToken;

        public GetDeviceTokenMessage(Client client, BinaryReader br) : base(client, br)
        {
            Decrypt();
        }

        public override void Decode()
        {
            using (var br = new BinaryReader(new MemoryStream(GetData())))
            {
                UserToken = br.ReadString();
                Unknown1 = br.ReadInt32();
            }
        }

        public override void Process(Level level)
        {
            var p = new SetDeviceTokenMessage(Client);
            p.UserToken = UserToken;
            PacketManager.ProcessOutgoingPacket(p);
        }
    }
}