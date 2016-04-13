using System.IO;
using UCS.Core;
using UCS.Helpers;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    internal class AskForAllianceDataMessage : Message
    {
        private long m_vAllianceId;

        public AskForAllianceDataMessage(Client client, BinaryReader br)
            : base(client, br)
        {
            Decrypt();
        }

        public override void Decode()
        {
            using (var br = new BinaryReader(new MemoryStream(GetData())))
            {
                m_vAllianceId = br.ReadInt64WithEndian();
            }
        }

        public override void Process(Level level)
        {
            var alliance = ObjectManager.GetAlliance(m_vAllianceId);
            if (alliance != null)
            {
                PacketManager.ProcessOutgoingPacket(new AllianceDataMessage(Client, alliance));
            }
        }
    }
}