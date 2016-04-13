using System.IO;
using UCS.Core;
using UCS.Helpers;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    internal class RequestAvatarNameChange : Message
    {
        public RequestAvatarNameChange(Client client, BinaryReader br) : base(client, br)
        {
            Decrypt();
        }

        public string PlayerName { get; set; }

        public byte Unknown1 { get; set; }

        public override void Decode()
        {
            using (var br = new BinaryReader(new MemoryStream(GetData())))
            {
                PlayerName = br.ReadScString();
            }
        }

        public override void Process(Level level)
        {
            var id = level.GetPlayerAvatar().GetId();
            var l = ResourcesManager.GetPlayer(id, true);
            if (l != null)
            {
                l.GetPlayerAvatar().SetName(PlayerName);
                var p = new AvatarNameChangeOkMessage(l.GetClient());
                p.SetAvatarName(PlayerName);
                PacketManager.ProcessOutgoingPacket(p);
            }
        }
    }
}