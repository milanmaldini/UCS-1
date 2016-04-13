using System.Collections.Generic;
using System.Text;
using UCS.Core;
using UCS.Helpers;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    //Packet 24133
    internal class NpcDataMessage : Message
    {
        public NpcDataMessage(Client client, Level level, AttackNpcMessage cnam) : base(client)
        {
            SetMessageType(24133);
            Player = level;
            LevelId = cnam.LevelId;
            JsonBase = ObjectManager.NpcLevels[LevelId];
        }

        public string JsonBase { get; set; }

        public int LevelId { get; set; }

        public Level Player { get; set; }

        public override void Encode()
        {
            var data = new List<byte>();

            data.AddInt32(0);
            data.AddInt32(JsonBase.Length);
            data.AddRange(Encoding.ASCII.GetBytes(JsonBase));
            data.AddRange(Player.GetPlayerAvatar().Encode());
            data.AddInt32(0);
            data.AddInt32(LevelId);

            Encrypt(data.ToArray());
        }
    }
}