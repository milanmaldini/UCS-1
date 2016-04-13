using System.IO;
using UCS.Core;
using UCS.GameFiles;
using UCS.Helpers;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    //Commande 0x1FD
    internal class CancelUnitProductionCommand : Command
    {
        public CancelUnitProductionCommand(BinaryReader br)
        {
            BuildingId = br.ReadInt32WithEndian(); //buildingId - 0x1DCD6500;
            Unknown1 = br.ReadUInt32WithEndian();
            UnitType = br.ReadInt32WithEndian();
            Count = br.ReadInt32WithEndian();
            Unknown3 = br.ReadUInt32WithEndian();
            Unknown4 = br.ReadUInt32WithEndian();
        }

        public int BuildingId { get; set; }
        public int Count { get; set; }
        public int UnitType { get; set; }
        public uint Unknown1 { get; set; } //00 00 00 00

        //00 3D 09 00
        //00 00 00 01
        public uint Unknown3 { get; set; } //00 00 00 00

        public uint Unknown4 { get; set; } //00 00 34 E4

        //00 00 01 FD 1D CD 65 05 00 00 00 00 00 3D 09 09 00 00 00 01 00 00 00 00 00 00 04 24

        public override void Execute(Level level)
        {
            var go = level.GameObjectManager.GetGameObjectByID(BuildingId);
            if (Count > 0)
            {
                var b = (Building) go;
                var c = b.GetUnitProductionComponent();
                var cd = (CombatItemData) ObjectManager.DataTables.GetDataById(UnitType);
                do
                {
                    //Ajouter gestion remboursement ressources
                    c.RemoveUnit(cd);
                    Count--;
                } while (Count > 0);
            }
        }
    }
}