using System.IO;
using UCS.Helpers;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    internal class ChangeAvatarNameMessage : Message
    {
        public ChangeAvatarNameMessage(Client client, BinaryReader br)
            : base(client, br)
        {
            Decrypt();
        }

        public string PlayerName { get; set; }

        public int PlayerNameLength { get; set; }

        public byte Unknown1 { get; set; }

        public override void Decode()
        {
            using (var br = new BinaryReader(new MemoryStream(GetData())))
            {
                PlayerName = br.ReadScString();
                Unknown1 = br.ReadByte();
            }
        }

        public override void Process(Level level)
        {
            level.GetPlayerAvatar().SetName(PlayerName);
            var p = new AvatarNameChangeOkMessage(Client);
            p.SetAvatarName(level.GetPlayerAvatar().GetAvatarName());
            PacketManager.ProcessOutgoingPacket(p);
        }
    }
}