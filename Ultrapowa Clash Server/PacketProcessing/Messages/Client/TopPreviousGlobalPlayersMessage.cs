using System.IO;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    internal class TopPreviousGlobalPlayersMessage : Message
    {
        public TopPreviousGlobalPlayersMessage(Client client, BinaryReader br) : base(client, br)
        {
            Decrypt();
        }

        public override void Decode()
        {
        }

        public override void Process(Level level)
        {
            PacketManager.ProcessOutgoingPacket(new PreviousGlobalPlayersMessage(Client));
        }
    }
}