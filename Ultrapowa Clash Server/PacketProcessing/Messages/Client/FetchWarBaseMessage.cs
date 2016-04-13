using System.IO;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    //Packet 15000

    internal class FetchWarBaseMessage : Message
    {
        public FetchWarBaseMessage(Client client, BinaryReader br) : base(client, br)
        {
            Decrypt();
        }

        public override void Decode()
        {
        }

        public override void Process(Level level)
        {
        }
    }
}