using System.IO;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    internal class TopGlobalPlayersMessage : Message
    {
        public TopGlobalPlayersMessage(Client client, BinaryReader br) : base(client, br)
        {
            Decrypt();
        }

        public override void Decode()
        {
        }

        public override void Process(Level level)
        {
            PacketManager.ProcessOutgoingPacket(new GlobalPlayersMessage(Client));
            // PacketManager.ProcessOutgoingPacket(new LeaguePlayersMessage(Client));
        }
    }
}