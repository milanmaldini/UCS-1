using UCS.Core;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    internal class BecomeLeaderGameOpCommand : GameOpCommand
    {
        private readonly string[] m_vArgs;

        public BecomeLeaderGameOpCommand(string[] args)
        {
            m_vArgs = args;
            SetRequiredAccountPrivileges(5);
        }

        public override void Execute(Level level)
        {
            if (level.GetAccountPrivileges() >= GetRequiredAccountPrivileges())
            {
                var clanid = level.GetPlayerAvatar().GetAllianceId();
                if (clanid != 0)
                {
                    foreach (
                        var pl in
                            ObjectManager.GetAlliance(level.GetPlayerAvatar().GetAllianceId()).GetAllianceMembers())
                        if (pl.GetRole() == 2)
                        {
                            pl.SetRole(4);
                            break;
                        }
                    level.GetPlayerAvatar().SetAllianceRole(2);
                }
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