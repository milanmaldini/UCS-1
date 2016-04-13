using System.IO;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    internal class JoinRequestAllianceMessage : Message
    {
        // 14317
        public JoinRequestAllianceMessage(Client client, BinaryReader br) : base(client, br)
        {
            Decrypt();
        }

        public static bool Unknown1 { get; set; }
        public static long Unknown2 { get; set; }
        public static int Unknown3 { get; set; }
        public static string Message { get; set; }

        public override void Decode()
        {
            using (var br = new BinaryReader(new MemoryStream(GetData())))
            {
                Unknown1 = br.ReadBoolean();
                Unknown2 = br.ReadInt64();
                Unknown3 = br.ReadInt16();
                Message = br.ReadString();
            }
        }

        public override void Process(Level level)
        {
            PacketManager.ProcessOutgoingPacket(new AnswerJoinRequestAllianceMessage(Client));
        }
    }
}