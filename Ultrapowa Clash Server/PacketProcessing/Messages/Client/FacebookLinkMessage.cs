using System;
using System.IO;
using System.Text;
using UCS.Helpers;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    internal class FacebookLinkMessage : Message
    {
        public FacebookLinkMessage(Client client, BinaryReader br) : base(client, br)
        {
            Decrypt();
        }

        public override void Decode()
        {
            using (var br = new BinaryReader(new MemoryStream(GetData())))
            {
                /*
                Console.WriteLine("Boolean -> " + br.ReadBoolean());
                Console.WriteLine("Unknown 1 -> " + br.ReadInt32());
                Console.WriteLine("Unknown 2 -> " + br.ReadInt32());
                Console.WriteLine("Unknown 3 -> " + br.ReadInt32());
                Console.WriteLine("Unknown 4 -> " + br.ReadInt16());
                Console.WriteLine("Token -> " + br.ReadString());
                */
                Console.WriteLine(Encoding.ASCII.GetString(br.ReadAllBytes()));
            }
        }

        public override void Process(Level level)
        {
        }
    }
}