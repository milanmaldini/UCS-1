using System;
using System.IO;
using System.Linq;
using UCS.Core;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    //Packet 14308
    internal class LeaveAllianceMessage : Message
    {
        public static bool done;

        public LeaveAllianceMessage(Client client, BinaryReader br) : base(client, br)
        {
            Decrypt();
        }

        public override void Decode()
        {
        }

        public override void Process(Level level)
        {
            var alliance = ObjectManager.GetAlliance(level.GetPlayerAvatar().GetAllianceId());
            alliance.RemoveMember(level.GetPlayerAvatar().GetId());

            if (alliance.GetAllianceMembers().Count == 0)
            {
                DatabaseManager.Singelton.RemoveAlliance(alliance);
            }
            else if (level.GetPlayerAvatar().GetAllianceRole() == 2)
            {
                var members = alliance.GetAllianceMembers();
                foreach (var player in members.Where(player => player.GetRole() >= 3))
                {
                    player.SetRole(2);
                    done = true;
                    break;
                }
                if (!done)
                {
                    var count = alliance.GetAllianceMembers().Count;
                    var rnd = new Random();
                    var id = rnd.Next(1, count);
                    while (id != level.GetPlayerAvatar().GetId())
                        id = rnd.Next(1, count);
                    var loop = 0;
                    foreach (var player in members)
                    {
                        loop++;
                        if (loop == id)
                        {
                            player.SetRole(2);
                            break;
                        }
                    }
                }
            }
            level.GetPlayerAvatar().SetAllianceId(0);
            PacketManager.ProcessOutgoingPacket(new LeaveAllianceOkMessage(Client, alliance));
        }
    }
}