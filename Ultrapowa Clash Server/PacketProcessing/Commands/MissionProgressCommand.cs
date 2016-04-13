using System.IO;

namespace UCS.PacketProcessing
{
    //Commande 0x207
    internal class MissionProgressCommand : Command
    {
        public MissionProgressCommand(BinaryReader br)
        {
            /*
            MissionId = br.ReadUInt32WithEndian(); //missionId - 0x1406F40;
            Unknown1 = br.ReadUInt32WithEndian();
            */
        }

        public uint MissionId { get; set; }
        public uint Unknown1 { get; set; }
    }
}