using System.Collections.Generic;
using UCS.Helpers;

namespace UCS.PacketProcessing
{
    internal class LeaguePlayersMessage : Message
    {
        public LeaguePlayersMessage(Client client) : base(client)
        {
            SetMessageType(24503);
        }

        public override void Encode()
        {
            var data = new List<byte>();
            data.AddInt32(1);
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);
            Encrypt(data.ToArray());
        }
    }
}