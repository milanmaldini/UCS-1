using System.Collections.Generic;
using UCS.Helpers;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    //Packet 24334
    internal class AvatarProfileMessage : Message
    {
        private Level m_vLevel;

        public AvatarProfileMessage(Client client)
            : base(client)
        {
            SetMessageType(24334);
        }

        public override void Encode()
        {
            var pack = new List<byte>();
            var ch = new ClientHome(m_vLevel.GetPlayerAvatar().GetId());
            ch.SetHomeJSON(m_vLevel.SaveToJSON());

            pack.AddRange(m_vLevel.GetPlayerAvatar().Encode());
            pack.AddInt32(ch.GetHomeJSON().Length + 4);
            pack.AddInt32(unchecked((int) 0xFFFF0000));
            pack.AddRange(ch.GetHomeJSON());

            pack.AddInt32(200);
            pack.AddInt32(100);
            pack.AddInt32(0);
            pack.AddInt32(0);
            pack.Add(0);
            Encrypt(pack.ToArray());
        }

        public void SetLevel(Level level)
        {
            m_vLevel = level;
        }
    }
}