using System.IO;

namespace UCS.PacketProcessing
{
    internal class ToggleHeroCommand : Command
    {
        // This command is received when the two toggle are pushed fastly, so we need to set AttackMode + HeroState
        public ToggleHeroCommand(BinaryReader br)
        {
        }
    }
}