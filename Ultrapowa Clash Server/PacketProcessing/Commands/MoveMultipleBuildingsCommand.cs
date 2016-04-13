using System.Collections.Generic;
using System.IO;
using UCS.Helpers;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    internal class BuildingToMove
    {
        public int GameObjectId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }

    //Commande 0x215
    internal class MoveMultipleBuildingsCommand : Command
    {
        private readonly List<BuildingToMove> m_vBuildingsToMove;

        public MoveMultipleBuildingsCommand(BinaryReader br)
        {
            m_vBuildingsToMove = new List<BuildingToMove>();
            var buildingCount = br.ReadInt32WithEndian();
            for (var i = 0; i < buildingCount; i++)
            {
                var buildingToMove = new BuildingToMove();
                buildingToMove.X = br.ReadInt32WithEndian();
                buildingToMove.Y = br.ReadInt32WithEndian();
                buildingToMove.GameObjectId = br.ReadInt32WithEndian();
                m_vBuildingsToMove.Add(buildingToMove);
            }
            br.ReadInt32WithEndian();
        }

        public override void Execute(Level level)
        {
            foreach (var buildingToMove in m_vBuildingsToMove)
            {
                var go = level.GameObjectManager.GetGameObjectByID(buildingToMove.GameObjectId);
                go.SetPositionXY(buildingToMove.X, buildingToMove.Y);
            }
        }
    }
}