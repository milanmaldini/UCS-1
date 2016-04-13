using System.Collections.Generic;
using UCS.Helpers;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    //Packet 24340
    internal class PromoteAllianceMemberOkMessage : Message
    {
        private readonly long m_vId;
        private readonly int m_vRole;

        public PromoteAllianceMemberOkMessage(Client client, Level level, PromoteAllianceMemberMessage pam)
            : base(client)
        {
            SetMessageType(24306);
            m_vId = pam.m_vId;
            m_vRole = pam.m_vRole;
        }

        public override void Encode()
        {
            var pack = new List<byte>();
            pack.AddInt64(m_vId);
            pack.AddInt32(m_vRole);
            Encrypt(pack.ToArray());
        }
    }
}