using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UCS.Core;
using UCS.Helpers;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    //14715
    internal class SendGlobalChatLineMessage : Message
    {
        public SendGlobalChatLineMessage(Client client, BinaryReader br) : base(client, br)
        {
            Decrypt();
        }

        public string Message { get; set; }

        public override void Decode()
        {
            using (var br = new BinaryReader(new MemoryStream(GetData())))
            {
                Message = br.ReadScString();
            }
        }

        public override void Process(Level level)
        {
            if (Message.Length > 0)
            {
                if (Message[0] == '/')
                {
                    var obj = GameOpCommandFactory.Parse(Message);
                    if (obj != null)
                    {
                        var player = "";
                        if (level != null)
                            player += " (" + level.GetPlayerAvatar().GetId() + ", " +
                                      level.GetPlayerAvatar().GetAvatarName() + ")";
                        Debugger.WriteLine("\t" + obj.GetType().Name + player);
                        ((GameOpCommand) obj).Execute(level);
                    }
                }
                else
                {
                    if (File.Exists(@"filter.ucs"))
                        //If you rename filter.ucs, you can simply deactivate with it the global Chat and send a Message to the player.
                    {
                        var senderId = level.GetPlayerAvatar().GetId();
                        var senderName = level.GetPlayerAvatar().GetAvatarName();

                        var badwords = new List<string>();
                        var r = new StreamReader(@"filter.ucs");
                        var line = "";
                        while ((line = r.ReadLine()) != null)
                        {
                            badwords.Add(line);
                        }
                        var badword = badwords.Any(s => Message.Contains(s));

                        if (badword)
                        {
                            var p = new GlobalChatLineMessage(level.GetClient());
                            p.SetPlayerId(0);
                            p.SetPlayerName("UCS Chat Filter System");
                            p.SetChatMessage("DETECTED BAD WORD! PLEASE AVOID USING BAD WORDS!");
                            PacketManager.ProcessOutgoingPacket(p);
                            return;
                        }

                        foreach (var onlinePlayer in ResourcesManager.GetOnlinePlayers())
                        {
                            var p = new GlobalChatLineMessage(onlinePlayer.GetClient());
                            if (onlinePlayer.GetAccountPrivileges() > 0)
                                p.SetPlayerName(senderName + " #" + senderId);
                            else
                                p.SetPlayerName(senderName);

                            p.SetChatMessage(Message);
                            p.SetPlayerId(senderId);
                            p.SetLeagueId(level.GetPlayerAvatar().GetLeagueId());
                            p.SetAlliance(ObjectManager.GetAlliance(level.GetPlayerAvatar().GetAllianceId()));
                            PacketManager.ProcessOutgoingPacket(p);
                        }

                        Console.WriteLine("[UCS][{0}]{1}", level.GetPlayerAvatar().GetAvatarName(), Message);
                    }
                    else
                    {
                        var p = new GlobalChatLineMessage(level.GetClient());
                        p.SetPlayerId(0);
                        p.SetPlayerName("UCS Chat System");
                        p.SetChatMessage(
                            "The Global Chat is currently disabled. Please try again later! For more Informations, check the Server Status!");
                        PacketManager.ProcessOutgoingPacket(p);
                    }
                }
            }
        }
    }
}