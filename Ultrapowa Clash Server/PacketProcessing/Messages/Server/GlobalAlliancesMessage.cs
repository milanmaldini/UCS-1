using System;
using System.Collections.Generic;
using System.Linq;
using UCS.Core;
using UCS.Helpers;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    internal class GlobalAlliancesMessage : Message
    {
        private List<Alliance> m_vAlliances;

        public GlobalAlliancesMessage(Client client) : base(client)
        {
            SetMessageType(24401);
        }

        public override void Encode()
        {
            var data = new List<byte>();
            var packet1 = new List<byte>();

            var i = 1;
            foreach (var alliance in ObjectManager.GetInMemoryAlliances().OrderByDescending(t => t.GetScore()))
            {
                packet1.AddInt64(alliance.GetAllianceId());
                packet1.AddString(alliance.GetAllianceName());
                packet1.AddInt32(i);
                packet1.AddInt32(alliance.GetScore());
                packet1.AddInt32(i);
                packet1.AddInt32(alliance.GetAllianceBadgeData());
                packet1.AddInt32(alliance.GetAllianceMembers().Count);
                packet1.AddInt32(0);
                packet1.AddInt32(alliance.GetAllianceLevel());
                if (i >= 200)
                    break;
                i++;
            }

            data.AddInt32(i - 1);
            data.AddRange(packet1);

            data.AddInt32((int) TimeSpan.FromDays(1).TotalSeconds);
            data.AddInt32(3);
            data.AddInt32(50000);
            data.AddInt32(30000);
            data.AddInt32(15000);
            Encrypt(data.ToArray());
        }
    }
}