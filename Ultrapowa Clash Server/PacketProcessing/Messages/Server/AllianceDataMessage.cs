using System.Collections.Generic;
using UCS.Helpers;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    //Packet 24301
    internal class AllianceDataMessage : Message
    {
        private readonly Alliance m_vAlliance;

        public AllianceDataMessage(Client client, Alliance alliance)
            : base(client)
        {
            SetMessageType(24301);
            m_vAlliance = alliance;
        }

        public override void Encode()
        {
            var pack = new List<byte>();

            var allianceMembers = m_vAlliance.GetAllianceMembers(); //avoid concurrent access issues

            pack.AddRange(m_vAlliance.EncodeFullEntry());
            pack.AddString(m_vAlliance.GetAllianceDescription());
            pack.AddInt32(0);
            pack.Add(0);

            pack.AddInt32(allianceMembers.Count);
            foreach (var allianceMember in allianceMembers)
                pack.AddRange(allianceMember.Encode());
            Encrypt(pack.ToArray());
        }
    }
}