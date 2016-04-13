using UCS.Core;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing.GameOpCommands
{
    internal class SaveAllGameOpCommand : GameOpCommand
    {
        private static DatabaseManager m_vDatabase;
        private readonly string[] m_vArgs;

        public SaveAllGameOpCommand(string[] args)
        {
            m_vArgs = args;
            SetRequiredAccountPrivileges(3);
        }

        public override void Execute(Level level)
        {
            if (level.GetAccountPrivileges() >= GetRequiredAccountPrivileges())
            {
                //Use this Command before you restart your server!

                /* Starting saving of players */
                var pm = new GlobalChatLineMessage(level.GetClient());
                pm.SetChatMessage("Starting saving process of every player!");
                pm.SetPlayerId(0);
                pm.SetLeagueId(22);
                pm.SetPlayerName("UCS Bot");
                PacketManager.ProcessOutgoingPacket(pm);
                DatabaseManager.Singelton.Save(ResourcesManager.GetInMemoryLevels());
                var p = new GlobalChatLineMessage(level.GetClient());
                /* Confirmation */
                p.SetChatMessage("All Players are saved!");
                p.SetPlayerId(0);
                p.SetLeagueId(22);
                p.SetPlayerName("UCS Bot");
                PacketManager.ProcessOutgoingPacket(p);
                /* Starting saving of Clans */
                var pmm = new GlobalChatLineMessage(level.GetClient());
                pmm.SetPlayerId(0);
                pmm.SetLeagueId(22);
                pmm.SetPlayerName("UCS Bot");
                pmm.SetChatMessage("Starting with saving of every Clan!");
                PacketManager.ProcessOutgoingPacket(pmm);
                /* Confirmation */
                DatabaseManager.Singelton.Save(ObjectManager.GetInMemoryAlliances());
                var pmp = new GlobalChatLineMessage(level.GetClient());
                pmp.SetPlayerId(0);
                pmp.SetLeagueId(22);
                pmp.SetPlayerName("UCS Bot");
                pmp.SetChatMessage("All Clans are saved!");
                PacketManager.ProcessOutgoingPacket(pmp);
            }
            else
            {
                var p = new GlobalChatLineMessage(level.GetClient());
                p.SetChatMessage("GameOp command failed. Access to Admin GameOP is prohibited.");
                p.SetPlayerId(0);
                p.SetLeagueId(22);
                p.SetPlayerName("UCS Bot");
                PacketManager.ProcessOutgoingPacket(p);
            }
        }
    }
}