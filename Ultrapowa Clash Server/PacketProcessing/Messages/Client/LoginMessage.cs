using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using UCS.Core;
using UCS.Helpers;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    //Packet 10101
    internal class LoginMessage : Message
    {
        public string AdvertisingGUID;
        public string AndroidDeviceID;
        public bool banned;
        public string ClientVersion;
        public int ContentVersion;
        public string DeviceModel;
        public string FacebookDistributionID;
        public bool IsAdvertisingTrackingEnabled;
        public string Language;
        public int LocaleKey;
        public string MacAddress;
        public int MajorVersion;
        public string MasterHash;
        public int MinorVersion;
        public string OpenUDID;
        public string OSVersion;
        public byte[] PlainText;
        public int Seed;
        public int Unknown;
        public string Unknown1;
        public byte Unknown2;
        public string Unknown3;
        public byte Unknown4;
        public string Unknown5;
        public string Unknown6;
        public long UserID;
        public string UserToken;
        public string VendorGUID;

        public LoginMessage(Client client, BinaryReader br) : base(client, br)
        {
            Decrypt();
        }

        public override void Decode()
        {
            if (Client.CState == 1)
            {
                using (var reader = new CoCSharpPacketReader(new MemoryStream(GetData())))
                {
                    UserID = reader.ReadInt64();
                    UserToken = reader.ReadString();
                    MajorVersion = reader.ReadInt32();
                    ContentVersion = reader.ReadInt32();
                    MinorVersion = reader.ReadInt32();
                    MasterHash = reader.ReadString();
                    Unknown1 = reader.ReadString();
                    OpenUDID = reader.ReadString();
                    MacAddress = reader.ReadString();
                    DeviceModel = reader.ReadString();
                    LocaleKey = reader.ReadInt32();
                    Language = reader.ReadString();
                    AdvertisingGUID = reader.ReadString();
                    OSVersion = reader.ReadString();
                    Unknown2 = reader.ReadByte();
                    Unknown3 = reader.ReadString();
                    AndroidDeviceID = reader.ReadString();
                    FacebookDistributionID = reader.ReadString();
                    IsAdvertisingTrackingEnabled = reader.ReadBoolean();
                    VendorGUID = reader.ReadString();
                    Seed = reader.ReadInt32();
                    Unknown4 = reader.ReadByte();
                    Unknown5 = reader.ReadString();
                    Unknown6 = reader.ReadString();
                    ClientVersion = reader.ReadString();
                }
            }
        }

        public override void Process(Level level)
        {
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["maintenanceMode"]) || banned || Client.CState == 0)
            {
                var p = new LoginFailedMessage(Client);
                p.SetErrorCode(10);
                PacketManager.ProcessOutgoingPacket(p);
                return;
            }

            var versionData = ConfigurationManager.AppSettings["clientVersion"].Split('.');
            if (versionData.Length >= 2)
            {
                var cv = ClientVersion.Split('.');
                if (cv[0] != versionData[0] || cv[1] != versionData[1])
                {
                    var p = new LoginFailedMessage(Client);
                    p.SetErrorCode(8);
                    p.SetUpdateURL(Convert.ToString(ConfigurationManager.AppSettings["UpdateUrl"]));
                    PacketManager.ProcessOutgoingPacket(p);
                    return;
                }
            }
            else
            {
                Debugger.WriteLine("[UCS][10101] Connection failed. UCS config key clientVersion is not properly set.");
            }

            if (Convert.ToBoolean(ConfigurationManager.AppSettings["useCustomPatch"]))
            {
                if (MasterHash != ObjectManager.FingerPrint.sha)
                {
                    var p = new LoginFailedMessage(Client);
                    p.SetErrorCode(7);
                    p.SetResourceFingerprintData(ObjectManager.FingerPrint.SaveToJson());
                    p.SetContentURL(ConfigurationManager.AppSettings["patchingServer"]);
                    p.SetUpdateURL("http://www.ultrapowa.com/client");
                    PacketManager.ProcessOutgoingPacket(p);
                    return;
                }
            }

            level = ResourcesManager.GetPlayer(UserID);
            if (level != null)
            {
                if (level.GetAccountStatus() == 99)
                {
                    var p = new LoginFailedMessage(Client);
                    p.SetErrorCode(11);
                    PacketManager.ProcessOutgoingPacket(p);
                    return;
                }
            }
            else
            {
                level = ObjectManager.CreateAvatar(UserID, UserToken);
                var tokenSeed = new byte[20];
                new Random().NextBytes(tokenSeed);
                using (SHA1 sha = new SHA1CryptoServiceProvider())
                {
                    UserToken = BitConverter.ToString(sha.ComputeHash(tokenSeed)).Replace("-", string.Empty);
                    level.GetPlayerAvatar().SetToken(UserToken);
                }
            }

            Client.ClientSeed = Seed;
            ResourcesManager.LogPlayerIn(level, Client);
            level.Tick();
            if (level.GetPlayerAvatar().GetUserToken() == UserToken)
            {
                var loginOk = new LoginOkMessage(Client);
                var avatar = level.GetPlayerAvatar();
                loginOk.SetAccountId(avatar.GetId());
                loginOk.SetPassToken(UserToken);
                loginOk.SetServerMajorVersion(MajorVersion);
                loginOk.SetServerBuild(MinorVersion);
                loginOk.SetContentVersion(ContentVersion);
                loginOk.SetServerEnvironment("prod");
                loginOk.SetDaysSinceStartedPlaying(10);
                loginOk.SetServerTime(
                    Math.Round(level.GetTime().Subtract(new DateTime(1970, 1, 1)).TotalSeconds * 1000).ToString());
                loginOk.SetAccountCreatedDate("1414003838000");
                loginOk.SetStartupCooldownSeconds(0);
                loginOk.SetCountryCode("EN");
                PacketManager.ProcessOutgoingPacket(loginOk);

                if (level.GetAccountPrivileges() >= 3)
                    level.GetPlayerAvatar().SetLeagueId(22);

                var alliance = ObjectManager.GetAlliance(level.GetPlayerAvatar().GetAllianceId());
                if (alliance == null)
                    level.GetPlayerAvatar().SetAllianceId(0);

                PacketManager.ProcessOutgoingPacket(new OwnHomeDataMessage(Client, level));
                if (alliance != null)
                    PacketManager.ProcessOutgoingPacket(new AllianceStreamMessage(Client, alliance));
                PacketManager.ProcessOutgoingPacket(new BookmarkMessage(Client));
            }
            else
            {
                var p = new LoginFailedMessage(Client);
                p.SetErrorCode(0);
                PacketManager.ProcessOutgoingPacket(p);
            }
        }
    }
}
