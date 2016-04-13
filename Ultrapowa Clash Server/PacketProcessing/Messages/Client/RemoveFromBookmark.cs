using System;
using System.IO;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    internal class RemoveFromBookmarkMessage : Message
    {
        public RemoveFromBookmarkMessage(Client client, BinaryReader br) : base(client, br)
        {
            Decrypt();
        }

        public override void Decode()
        {
            using (var br = new BinaryReader(new MemoryStream(GetData())))
                Console.WriteLine("ID DU CLAN -> " + br.ReadInt32());
        }

        public override void Process(Level level)
        {
            PacketManager.ProcessOutgoingPacket(new BookmarkRemoveAllianceMessage(Client));
        }
    }
}