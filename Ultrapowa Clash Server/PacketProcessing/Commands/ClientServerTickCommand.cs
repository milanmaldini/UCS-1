using System.IO;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    internal class ClientServerTickCommand : Command
    {
        public ClientServerTickCommand(BinaryReader br)
        {
            Unknown1 = br.ReadInt32();
            Tick = br.ReadInt32();
        }

        public static int Unknown1 { get; set; }
        public static int Tick { get; set; }

        public override void Execute(Level level)
        {
        }
    }
}