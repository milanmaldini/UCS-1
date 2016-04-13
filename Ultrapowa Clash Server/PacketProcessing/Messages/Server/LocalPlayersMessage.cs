using System.Collections.Generic;
using System.Linq;
using UCS.Core;
using UCS.Helpers;

namespace UCS.PacketProcessing
{
    internal class LocalPlayersMessage : Message
    {
        public LocalPlayersMessage(Client client) : base(client)
        {
            SetMessageType(24404);
        }

        public override void Encode()
        {
            var packet = new List<byte>();
            var data = new List<byte>();

            var i = 1;
            foreach (
                var player in ResourcesManager.GetOnlinePlayers().OrderByDescending(t => t.GetPlayerAvatar().GetScore())
                )
            {
                var pl = player.GetPlayerAvatar();
                var id = pl.GetAllianceId();
                data.AddInt64(pl.GetId()); // The ID of the player
                data.AddString(pl.GetAvatarName()); // THe Name of the Player
                data.AddInt32(i); // Rank of the player
                data.AddInt32(pl.GetScore()); // Number of Trophies of the player
                data.AddInt32(i); // Up/Down from previous rank -> (int - 1)
                data.AddInt32(pl.GetAvatarLevel()); // The score of the player
                data.AddInt32(100); // The number of successed attack
                data.AddInt32(1); // Unknown1 Int32
                data.AddInt32(100); // Number of successed defenses
                data.AddInt32(1); // Activation of successed attacks
                data.AddInt32(pl.GetLeagueId()); // League of the player
                data.AddString("EN"); // Locales
                data.AddInt64(pl.GetId()); // Clan ID
                data.AddInt32(1); // Unknown2
                data.AddInt32(1); // Unknown3
                if (pl.GetAllianceId() > 0)
                {
                    data.Add(1); // 1 = Have an alliance | 0 = No alliance
                    data.AddInt64(pl.GetAllianceId()); // Alliance ID
                    data.AddString(ObjectManager.GetAlliance(id).GetAllianceName()); // Alliance Name
                    data.AddInt32(ObjectManager.GetAlliance(id).GetAllianceBadgeData()); // Alliance Badge
                }
                else
                    data.Add(0);
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