using System.Collections.Generic;
using UCS.Helpers;

namespace UCS.PacketProcessing
{
    internal class AllianceWarDataMessage : Message
    {
        public AllianceWarDataMessage(Client client) : base(client)
        {
            SetMessageType(24331);
        }

        public override void Encode()
        {
            var data = new List<byte>();
            data.AddInt32(0);
            data.AddInt32(0);

            data.AddInt64(1); // Team ID
            data.AddString("GobelinLand");
            data.AddInt32(0);
            data.AddInt32(1);
            data.Add(0);
            data.AddRange(new List<byte> {1, 2, 3, 4});
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);

            data.AddInt64(1); // Team ID
            data.AddString("GobelinLand");
            data.AddInt32(0);
            data.AddInt32(1);
            data.Add(0);
            data.AddRange(new List<byte> {1, 2, 3, 4});
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);

            data.AddInt64(11);

            data.AddInt32(0);
            data.AddInt32(0);

            data.AddInt32(1);
            data.AddInt32(3600);
            data.AddInt64(1);
            data.AddInt64(1);
            data.AddInt64(2);
            data.AddInt64(2);

            data.AddString("Berkan");
            data.AddString("Mimi");

            data.AddInt32(2);
            data.AddInt32(1);
            data.AddInt32(50);

            data.AddInt32(0);

            data.AddInt32(8);
            data.AddInt32(0);
            data.AddInt32(0);
            data.Add(0);
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);


            Encrypt(data.ToArray());
        }
    }
}