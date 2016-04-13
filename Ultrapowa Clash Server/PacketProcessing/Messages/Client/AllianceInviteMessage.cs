using System.IO;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    internal class AllianceInviteMessage : Message
    {
        public AllianceInviteMessage(Client client, BinaryReader br) : base(client, br)
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