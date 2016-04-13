using System.IO;

namespace UCS.PacketProcessing
{
    //Commande 0x203
    internal class CancelUpgradeUnitCommand : Command
    {
        public CancelUpgradeUnitCommand(BinaryReader br)
        {
            /*
            BuildingId = br.ReadUInt32WithEndian(); //buildingId - 0x1DCD6500;
            Unknown1 = br.ReadUInt32WithEndian();
            */
        }

        public uint BuildingId { get; set; }
        public uint Unknown1 { get; set; }
    }
}