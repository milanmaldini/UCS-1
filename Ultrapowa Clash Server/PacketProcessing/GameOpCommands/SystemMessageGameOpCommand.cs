using System;
using System.Linq;
using UCS.Core;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    internal class SystemMessageGameOpCommand : GameOpCommand
    {
        private readonly string[] m_vArgs;

        public SystemMessageGameOpCommand(string[] args)
        {
            m_vArgs = args;
            SetRequiredAccountPrivileges(1);
        }

        public override void Execute(Level level)
        {
            if (level.GetAccountPrivileges() >= GetRequiredAccountPrivileges())
            {
                if (m_vArgs.Length >= 1)
                {
                    var message = string.Join(" ", m_vArgs.Skip(1));
                    var avatar = level.GetPlayerAvatar();
                    var mail = new AllianceMailStreamEntry();
                    mail.SetId((int) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
                    mail.SetSenderId(avatar.GetId());
                    mail.SetSenderAvatarId(avatar.GetId());
                    mail.SetSenderName(avatar.GetAvatarName());
                    mail.SetIsNew(0);
                    mail.SetAllianceId(0);
                    mail.SetAllianceBadgeData(0);
                    mail.SetAllianceName("Administrator");
                    mail.SetMessage(message);
                    mail.SetSenderLevel(avatar.GetAvatarLevel());
                    mail.SetSenderLeagueId(avatar.GetLeagueId());

                    foreach (var onlinePlayer in ResourcesManager.GetOnlinePlayers())
                    {
                        var p = new AvatarStreamEntryMessage(onlinePlayer.GetClient());
                        p.SetAvatarStreamEntry(mail);
                        PacketManager.ProcessOutgoingPacket(p);
                    }
                }
            }
            else
            {
                SendCommandFailedMessage(level.GetClient());
            }
        }
    }
}