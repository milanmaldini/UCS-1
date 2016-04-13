using System.IO;
using UCS.Core;
using UCS.Helpers;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    //Packet 14306

    internal class PromoteAllianceMemberMessage : Message
    {
        public long m_vId;
        public int m_vRole;

        public PromoteAllianceMemberMessage(Client client, BinaryReader br) : base(client, br)
        {
            Decrypt();
        }

        public override void Decode()
        {
            using (var br = new BinaryReader(new MemoryStream(GetData())))
            {
                m_vId = br.ReadInt64WithEndian();
                m_vRole = br.ReadInt32WithEndian();
            }
        }

        public override void Process(Level level)
        {
            var target = ResourcesManager.GetPlayer(m_vId);
            var player = level.GetPlayerAvatar();
            var alliance = ObjectManager.GetAlliance(player.GetAllianceId());
            if (player.GetAllianceRole() == 2 || player.GetAllianceRole() == 4)
                if (player.GetAllianceId() == target.GetPlayerAvatar().GetAllianceId())
                {
                    target.GetPlayerAvatar().SetAllianceRole(m_vRole);
                    if (m_vRole == 2)
                        player.SetAllianceRole(4);
                }
            // PacketManager.ProcessOutgoingPacket(new AllianceDataMessage(Client, alliance));
            PacketManager.ProcessOutgoingPacket(new PromoteAllianceMemberOkMessage(Client, target, this));
        }
    }
}