using System;
using System.IO;
using System.Text;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    internal class AttackResultMessage : Message
    {
        public AttackResultMessage(Client client, BinaryReader br)
            : base(client, br)
        {
            Decrypt();
        }

        public override void Decode()
        {
            Console.WriteLine("Packet Attack Result : " + Encoding.UTF8.GetString(GetData()));
        }

        public override void Process(Level level)
        {
        }
    }
}