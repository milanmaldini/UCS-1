using System.IO;

namespace UCS.PacketProcessing
{
    //Commande 0x0214
    internal class NewShopItemsSeenCommand : Command
    {
        public NewShopItemsSeenCommand(BinaryReader br)
        {
            /*
            var NewShopItemNumber = br.ReadUInt32WithEndian();
            var Unknown1 = br.ReadUInt32WithEndian();
            var Unknown2 = br.ReadUInt32WithEndian();
            var Unknown3 = br.ReadUInt32WithEndian();
            */
        }

        public uint NewShopItemNumber { get; set; }
        public uint Unknown1 { get; set; }
        public uint Unknown2 { get; set; }
        public uint Unknown3 { get; set; }
    }
}