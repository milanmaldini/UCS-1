using System.IO;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    internal class UnknownCommand : Command
    {
        public UnknownCommand(BinaryReader br)
        {
            //Unknown1 = br.ReadInt32();
            //Tick = br.ReadInt32();
            //Packet = br.ReadAllBytes();
        }

        public static int Unknown1 { get; set; }
        public static int Tick { get; set; }
        public static byte[] Packet { get; set; }

        public override void Execute(Level level)
        {
            //Console.WriteLine("[CMD][0]     " + Unknown1);
            //Console.WriteLine("[CMD][0]     " + Tick);
            //Console.WriteLine("[CMD][0][FULL] " + Encoding.ASCII.GetString(Packet));
        }
    }
}