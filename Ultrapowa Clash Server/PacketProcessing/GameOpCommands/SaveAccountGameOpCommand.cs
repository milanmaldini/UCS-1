using UCS.Core;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    internal class SaveAccountGameOpCommand : GameOpCommand
    {
        private static DatabaseManager m_vDatabase;
        private readonly string[] m_vArgs;

        public SaveAccountGameOpCommand(string[] args)
        {
            m_vArgs = args;
            SetRequiredAccountPrivileges(5);
        }

        public override void Execute(Level level)
        {
            if (level.GetAccountPrivileges() >= GetRequiredAccountPrivileges())
            {
                DatabaseManager.Singelton.Save(level);
                var p = new GlobalChatLineMessage(level.GetClient());
                p.SetChatMessage("Game Successfuly Saved!");
                p.SetPlayerId(0);
                p.SetLeagueId(22);
                p.SetPlayerName("UCS Bot");
                PacketManager.ProcessOutgoingPacket(p);
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