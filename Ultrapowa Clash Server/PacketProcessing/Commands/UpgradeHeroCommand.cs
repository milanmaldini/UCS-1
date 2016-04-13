using System.IO;
using UCS.Core;
using UCS.Helpers;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    //Commande 0x020F
    internal class UpgradeHeroCommand : Command
    {
        public UpgradeHeroCommand(BinaryReader br)
        {
            BuildingId = br.ReadInt32WithEndian();
            Unknown1 = br.ReadUInt32WithEndian();
        }

        //00 00 02 0F 1D CD 65 09 00 01 03 63

        public int BuildingId { get; set; }
        public uint Unknown1 { get; set; }

        public override void Execute(Level level)
        {
            var ca = level.GetPlayerAvatar();
            var go = level.GameObjectManager.GetGameObjectByID(BuildingId);
            if (go != null)
            {
                var b = (Building) go;
                var hbc = b.GetHeroBaseComponent();
                if (hbc != null)
                {
                    if (hbc.CanStartUpgrading())
                    {
                        var hd = ObjectManager.DataTables.GetHeroByName(b.GetBuildingData().HeroType);
                        var currentLevel = ca.GetUnitUpgradeLevel(hd);
                        var rd = hd.GetUpgradeResource(currentLevel);
                        var cost = hd.GetUpgradeCost(currentLevel);
                        if (ca.HasEnoughResources(rd, cost))
                        {
                            if (level.HasFreeWorkers())
                            {
                                hbc.StartUpgrading();
                            }
                        }
                    }
                }
            }
        }
    }
}