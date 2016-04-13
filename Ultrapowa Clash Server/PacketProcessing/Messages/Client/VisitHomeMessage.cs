using System.IO;
using UCS.Core;
using UCS.Helpers;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    //Packet 14113
    internal class VisitHomeMessage : Message
    {
        public VisitHomeMessage(Client client, BinaryReader br)
            : base(client, br)
        {
            Decrypt();
        }

        public long AvatarId { get; set; }

        public override void Decode()
        {
            using (var br = new BinaryReader(new MemoryStream(GetData())))
            {
                AvatarId = br.ReadInt64WithEndian();
            }
        }

        public override void Process(Level level)
        {
            var targetLevel = ResourcesManager.GetPlayer(AvatarId);
            targetLevel.Tick();
            //Clan clan;
            PacketManager.ProcessOutgoingPacket(new VisitedHomeDataMessage(Client, targetLevel, level));
            //if (clan != null)*/
            //    PacketHandler.ProcessOutgoingPacket(new ServerAllianceChatHistory(this.Client, clan));
        }
    }
}