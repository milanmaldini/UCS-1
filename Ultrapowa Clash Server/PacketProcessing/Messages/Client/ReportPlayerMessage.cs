using System.IO;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    internal class ReportPlayerMessage : Message
    {
        public ReportPlayerMessage(Client client, BinaryReader br) : base(client, br)
        {
            Decrypt();
        }

        public override void Decode()
        {
            using (var br = new BinaryReader(new MemoryStream(GetData())))
            {
            }
        }

        public override void Process(Level level)
        {
        }
    }
}