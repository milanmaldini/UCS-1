using System.Collections.Generic;
using System.Linq;
using UCS.Core;
using UCS.Helpers;

namespace UCS.PacketProcessing
{
    internal class BookmarkListMessage : Message
    {
        public BookmarkListMessage(Client client) : base(client)
        {
            SetMessageType(24341);
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
                packet1.AddInt32(alliance.GetAllianceBadgeData());
                packet1.AddInt32(alliance.GetAllianceType());
                packet1.AddInt32(alliance.GetAllianceMembers().Count);
                packet1.AddInt32(alliance.GetScore());
                packet1.AddInt64(0); // ?
                packet1.AddInt64(0); // ?
                packet1.AddInt64(0); // ?
                packet1.AddInt64(0); // ?
                packet1.AddInt32(alliance.GetAllianceLevel()); //lvl du clan
                packet1.AddInt32(0); // ?
                i++;
            }
            data.AddInt32(i - 1);
            data.AddRange(packet1);
            Encrypt(data.ToArray());
        }
    }
}