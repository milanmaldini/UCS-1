using System.IO;
using UCS.Helpers;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    internal class SpeedUpHeroUpgradeCommand : Command
    {
        private readonly int m_vBuildingId;
        private int m_vUnknown1;

        public SpeedUpHeroUpgradeCommand(BinaryReader br)
        {
            m_vBuildingId = br.ReadInt32WithEndian(); //buildingId - 0x1DCD6500;
            m_vUnknown1 = br.ReadInt32WithEndian();
        }

        public override void Execute(Level level)
        {
            var ca = level.GetPlayerAvatar();
            var go = level.GameObjectManager.GetGameObjectByID(m_vBuildingId);

            if (go != null)
            {
                var b = (Building) go;
                var hbc = b.GetHeroBaseComponent();
                if (hbc != null)
                    hbc.SpeedUpUpgrade();
            }
        }
    }
}