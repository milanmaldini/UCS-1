using System.Collections.Generic;
using System.Linq;
using UCS.Helpers;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    //Packet 24311
    internal class AllianceStreamMessage : Message
    {
        private readonly Alliance m_vAlliance;

        public AllianceStreamMessage(Client client, Alliance alliance) : base(client)
        {
            SetMessageType(24311);
            m_vAlliance = alliance;
        }

        public override void Encode()
        {
            var pack = new List<byte>();
            var chatMessages = m_vAlliance.GetChatMessages().ToList();
            pack.AddInt32(chatMessages.Count);
            foreach (var chatMessage in chatMessages)
            {
                pack.AddRange(chatMessage.Encode());
            }
            Encrypt(pack.ToArray());
        }
    }
}