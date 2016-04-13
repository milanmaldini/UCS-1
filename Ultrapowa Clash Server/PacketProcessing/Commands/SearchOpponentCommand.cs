using System.IO;
using UCS.Core;
using UCS.Helpers;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    //Commande 700
    internal class SearchOpponentCommand : Command
    {
        public SearchOpponentCommand(BinaryReader br)
        {
            br.ReadInt32WithEndian();
            br.ReadInt32WithEndian();
            br.ReadInt32WithEndian();
        }

        //00 00 00 00 00 00 00 00 00 00 00 97

        public override void Execute(Level level)
        {
            var l = ObjectManager.GetRandomOnlinePlayer();
            if (l != null)
            {
                l.Tick();
                var p = new EnemyHomeDataMessage(level.GetClient(), l, level);
                PacketManager.ProcessOutgoingPacket(p);
            }
        }
    }
}