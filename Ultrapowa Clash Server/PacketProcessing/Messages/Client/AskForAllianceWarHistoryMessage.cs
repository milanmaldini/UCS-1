using System.IO;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    internal class AskForAllianceWarHistoryMessage : Message
    {
        public AskForAllianceWarHistoryMessage(Client client, BinaryReader br) : base(client, br)
        {
            Decrypt();
        }

        private static long AllianceID { get; set; }
        private static long WarID { get; set; }

        public override void Decode()
        {
            using (var br = new BinaryReader(new MemoryStream(GetData())))
            {
                AllianceID = br.ReadInt64();
                WarID = br.ReadInt64();
            }
        }

        public override void Process(Level level)
        {
            PacketManager.ProcessOutgoingPacket(new AllianceWarHistoryMessage(Client));
        }
    }
}