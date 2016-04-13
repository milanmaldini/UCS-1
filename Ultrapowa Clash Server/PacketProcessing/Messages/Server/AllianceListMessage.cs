using System.Collections.Generic;
using UCS.Helpers;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    //Packet 24310
    internal class AllianceListMessage : Message
    {
        private List<Alliance> m_vAlliances;
        private string m_vSearchString;

        public AllianceListMessage(Client client) : base(client)
        {
            SetMessageType(24310);
            m_vAlliances = new List<Alliance>();
        }

        public override void Encode()
        {
            var pack = new List<byte>();
            pack.AddString(m_vSearchString);
            pack.AddInt32(m_vAlliances.Count);
            foreach (var alliance in m_vAlliances)
            {
                pack.AddRange(alliance.EncodeFullEntry());
            }
            Encrypt(pack.ToArray());
        }

        public void SetAlliances(List<Alliance> alliances)
        {
            m_vAlliances = alliances;
        }

        public void SetSearchString(string searchString)
        {
            m_vSearchString = searchString;
        }
    }
}