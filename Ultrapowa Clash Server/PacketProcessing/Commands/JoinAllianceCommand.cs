using System.Collections.Generic;
using System.IO;
using UCS.Helpers;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    //Commande 0x001
    internal class JoinAllianceCommand : Command
    {
        private Alliance m_vAlliance;

        public JoinAllianceCommand()
        {
            //AvailableServerCommandMessage
        }

        public JoinAllianceCommand(BinaryReader br)
        {
            br.ReadInt64WithEndian();
            br.ReadScString();
            br.ReadInt32WithEndian();
            br.ReadByte();
            br.ReadInt32WithEndian();
            br.ReadInt32WithEndian();
            br.ReadInt32WithEndian();
        }

        public override byte[] Encode()
        {
            var data = new List<byte>();
            data.AddRange(m_vAlliance.EncodeHeader());
            return data.ToArray();
        }

        public void SetAlliance(Alliance alliance)
        {
            m_vAlliance = alliance;
        }
    }
}