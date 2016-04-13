using UCS.Core;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    internal class ShutdownServerGameOpCommand : GameOpCommand
    {
        private string[] m_vArgs;

        public ShutdownServerGameOpCommand(string[] args)
        {
            m_vArgs = args;
            SetRequiredAccountPrivileges(4);
        }

        public override void Execute(Level level)
        {
            if (level.GetAccountPrivileges() >= GetRequiredAccountPrivileges())
            {
                foreach (var onlinePlayer in ResourcesManager.GetOnlinePlayers())
                {
                    var p = new ShutdownStartedMessage(onlinePlayer.GetClient());
                    p.SetCode(5);
                    PacketManager.ProcessOutgoingPacket(p);
                }
            }
            else
            {
                SendCommandFailedMessage(level.GetClient());
            }
        }
    }
}