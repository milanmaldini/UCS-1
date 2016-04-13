using System.IO;
using UCS.Core;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    //Packet 14101
    internal class GoHomeMessage : Message
    {
        public GoHomeMessage(Client client, BinaryReader br) : base(client, br)
        {
            Decrypt();
        }

        public override void Decode()
        {
        }

        public override void Process(Level level)
        {
            level.Tick();

            var alliance = ObjectManager.GetAlliance(level.GetPlayerAvatar().GetAllianceId());
            //player.GetPlayerAvatar().Clean();
            PacketManager.ProcessOutgoingPacket(new OwnHomeDataMessage(Client, level));
            if (alliance != null)
                PacketManager.ProcessOutgoingPacket(new AllianceStreamMessage(Client, alliance));
        }
    }
}