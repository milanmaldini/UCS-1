using System.IO;
using UCS.GameFiles;
using UCS.Helpers;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    //Commande 600
    internal class PlaceAttackerCommand : Command
    {
        public PlaceAttackerCommand(BinaryReader br)
        {
            X = br.ReadInt32WithEndian();
            Y = br.ReadInt32WithEndian();
            Unit = (CharacterData) br.ReadDataReference();
            Unknown1 = br.ReadUInt32WithEndian();
        }

        //00 00 00 03
        //00 00 02 58 00 00 42 B9 00 00 57 01 00 3D 09 00 00 00 01 55
        //00 00 02 58 00 00 45 E8 00 00 56 1C 00 3D 09 00 00 00 01 5C
        //00 00 02 58 00 00 47 01 00 00 54 EB 00 3D 09 00 00 00 01 63

        public CharacterData Unit { get; set; }
        public uint Unknown1 { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public override void Execute(Level level)
        {
            var components = level.GetComponentManager().GetComponents(0);
            for (var i = 0; i < components.Count; i++)
            {
                var c = (UnitStorageComponent) components[i];
                if (c.GetUnitTypeIndex(Unit) != -1)
                {
                    var storageCount = c.GetUnitCountByData(Unit);
                    if (storageCount >= 0)
                    {
                        c.RemoveUnits(Unit, 1);
                        break;
                    }
                }
            }
        }
    }
}