using System;
using UCS.Core;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    internal class TestingGameOpCommand : GameOpCommand
    {
        private readonly string[] m_vArgs;

        public TestingGameOpCommand(string[] args)
        {
            m_vArgs = args;
        }

        public override void Execute(Level level)
        {
            var cm = new ShareStreamEntry();
            cm.SetId((int) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
            cm.SetSenderId(0);
            cm.SetHomeId(0);
            cm.SetSenderLeagueId(22);
            cm.SetSenderName("Mimi");
            cm.SetSenderRole(4);
            cm.SetMessage("Look this ! I killed it ahahah");
            cm.SetEnemyName("Mimi");
            cm.SetReplayjson("{'lol': 'mdr'}");
            var all = ObjectManager.GetAlliance(level.GetPlayerAvatar().GetAllianceId());
            all.AddChatMessage(cm);

            foreach (var onlinePlayer in ResourcesManager.GetOnlinePlayers())
            {
                if (onlinePlayer.GetPlayerAvatar().GetAllianceId() == level.GetPlayerAvatar().GetAllianceId())
                {
                    var p = new AllianceStreamEntryMessage(onlinePlayer.GetClient());
                    p.SetStreamEntry(cm);
                    PacketManager.ProcessOutgoingPacket(p);
                }
            }
        }
    }
}