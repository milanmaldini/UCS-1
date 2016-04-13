using UCS.Core;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    internal class MaxRessourcesCommand : GameOpCommand
    {
        public MaxRessourcesCommand(string[] Args)
        {
            SetRequiredAccountPrivileges(0);
        }

        public override void Execute(Level level)
        {
            if (level.GetAccountPrivileges() >= GetRequiredAccountPrivileges())
            {
                var p = level.GetPlayerAvatar();
                p.SetResourceCount(ObjectManager.DataTables.GetResourceByName("Gold"), 999999999);
                p.SetResourceCount(ObjectManager.DataTables.GetResourceByName("Elixir"), 999999999);
                p.SetResourceCount(ObjectManager.DataTables.GetResourceByName("DarkElixir"), 999999999);
                p.SetDiamonds(999999);
                var own = new OwnHomeDataMessage(level.GetClient(), level);
                PacketManager.ProcessOutgoingPacket(own);
            }
            else
                SendCommandFailedMessage(level.GetClient());
        }
    }
}