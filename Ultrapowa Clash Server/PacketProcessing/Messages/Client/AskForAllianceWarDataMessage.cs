using System.IO;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    // 14331
    internal class AskForAllianceWarDataMessage : Message
    {
        public AskForAllianceWarDataMessage(Client client, BinaryReader br) : base(client, br)
        {
            Decrypt();
        }

        public override void Decode()
        {
            using (var br = new BinaryReader(new MemoryStream(GetData())))
            {
            }
        }

        public override void Process(Level level)
        {
            PacketManager.ProcessOutgoingPacket(new AllianceWarDataMessage(Client));
        }
    }
}