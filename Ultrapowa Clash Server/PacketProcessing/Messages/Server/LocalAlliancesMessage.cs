using System.Collections.Generic;
using System.Linq;
using UCS.Core;
using UCS.Helpers;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    internal class LocalAlliancesMessage : Message
    {
        private List<Alliance> m_vAlliances;

        public LocalAlliancesMessage(Client client) : base(client)
        {
            SetMessageType(24402);
            m_vAlliances = new List<Alliance>();
        }

        public override void Encode()
        {
            var packet = new List<byte>();
            var data = new List<byte>();

            var i = 1;
            foreach (var alliance in ObjectManager.GetInMemoryAlliances().OrderByDescending(t => t.GetScore()))
            {
                var all = alliance.GetAllianceId();
                data.AddInt64(all);
                data.AddString(alliance.GetAllianceName());
                data.AddInt32(i);
                data.AddInt32(alliance.GetScore());
                data.AddInt32(i);
                data.AddInt32(alliance.GetAllianceBadgeData());
                data.AddInt32(alliance.GetAllianceMembers().Count);
                data.AddInt32(0);
                data.AddInt32(alliance.GetAllianceLevel());
                if (i >= 200)
                    break;
                i++;
            }
            packet.AddInt32(i - 1);
            packet.AddRange(data.ToArray());
            Encrypt(packet.ToArray());
        }
    }
}