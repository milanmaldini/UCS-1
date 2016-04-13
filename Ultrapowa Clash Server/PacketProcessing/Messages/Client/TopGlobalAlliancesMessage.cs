using System.IO;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    internal class TopGlobalAlliancesMessage : Message
    {
        public TopGlobalAlliancesMessage(Client client, BinaryReader br) : base(client, br)
        {
            Decrypt();
        }

        public override void Decode()
        {
        }

        public override void Process(Level level)
        {
            PacketManager.ProcessOutgoingPacket(new GlobalAlliancesMessage(Client));
            PacketManager.ProcessOutgoingPacket(new LocalAlliancesMessage(Client));
        }
    }
}