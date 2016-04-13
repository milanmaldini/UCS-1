using System.IO;
using UCS.Core;
using UCS.GameFiles;
using UCS.Helpers;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    //Commande 0x1FC 508
    internal class TrainUnitCommand : Command
    {
        public TrainUnitCommand(BinaryReader br)
        {
            BuildingId = br.ReadInt32WithEndian(); //buildingId - 0x1DCD6500;
            Unknown1 = br.ReadUInt32WithEndian();
            UnitType = br.ReadInt32WithEndian();
            Count = br.ReadInt32WithEndian();
            Unknown3 = br.ReadUInt32WithEndian();
        }

        public int BuildingId { get; set; }
        public int Count { get; set; }
        public int UnitType { get; set; }
        public uint Unknown1 { get; set; } //00 00 00 00

        //00 3D 09 03
        //00 00 00 01
        public uint Unknown3 { get; set; } //FF FF FF FF

        public override void Execute(Level level)
        {
            var go = level.GameObjectManager.GetGameObjectByID(BuildingId);
            if (Count > 0)
            {
                var b = (Building) go;
                var c = b.GetUnitProductionComponent();
                var cid = (CombatItemData) ObjectManager.DataTables.GetDataById(UnitType);
                do
                {
                    if (!c.CanAddUnitToQueue(cid))
                        break;
                    var trainingResource = cid.GetTrainingResource();
                    var ca = level.GetHomeOwnerAvatar();
                    var trainingCost = cid.GetTrainingCost(ca.GetUnitUpgradeLevel(cid));
                    if (!ca.HasEnoughResources(trainingResource, trainingCost))
                        break;
                    ca.SetResourceCount(trainingResource, ca.GetResourceCount(trainingResource) - trainingCost);
                    c.AddUnitToProductionQueue(cid);
                    Count--;
                } while (Count > 0);
            }
        }
    }
}