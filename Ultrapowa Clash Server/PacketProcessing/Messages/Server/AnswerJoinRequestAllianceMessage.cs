using System.Collections.Generic;

namespace UCS.PacketProcessing
{
    internal class AnswerJoinRequestAllianceMessage : Message
    {
        private readonly int m_vServerCommandType;
        private string m_vAvatarName;

        public AnswerJoinRequestAllianceMessage(Client client) : base(client)
        {
            SetMessageType(24317);
        }

        public override void Encode()
        {
            var pack = new List<byte>();

            Encrypt(pack.ToArray());
        }
    }
}