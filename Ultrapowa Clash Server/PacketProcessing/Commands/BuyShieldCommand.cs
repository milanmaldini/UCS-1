using System;
using System.IO;
using UCS.Helpers;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    //Commande 0x20A
    internal class BuyShieldCommand : Command
    {
        public BuyShieldCommand(BinaryReader br)
        {
            ShieldId = br.ReadInt32WithEndian(); //= shieldId - 0x01312D00;
            Unknown1 = br.ReadUInt32WithEndian();
        }

        public int ShieldId { get; set; }
        public uint Unknown1 { get; set; }

        public override void Execute(Level level)
        {
            Console.WriteLine(ShieldId);
            Console.WriteLine(Unknown1);
        }
    }
}