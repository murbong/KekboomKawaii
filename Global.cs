using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace KekboomKawaii
{

    [Flags]
    public enum ChatClassFlag : int
    {
        None = 0,
        World = 1,
        Team = 2,
        Recruit = 4,
        Guild = 8,
        Dojo = 16
    }
    public enum ChatClassEnum : int
    {
        None = 0,
        World = 1,
        Team = 3,
        Recruit = 7,
        Guild = 8,
        Dojo = 9
    }

    public enum GenderEnum : int
    {
        None = 0,
        Female = 1,
        Male = 0x70
    }
    public enum GuildPostEnum : int
    {
        Member = 0,
        Manager = 2,
        Admin = 5,
        Owner = 6
    }
    public static class Global
    {

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32")]
        private static extern int WritePrivateProfileString(string section, string key, string val, string filePath);


        public static void ShowToast(string appId, string title, string message, string image)
        {
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(
            string.IsNullOrEmpty(image) ? ToastTemplateType.ToastText02 : ToastTemplateType.ToastImageAndText02);

            XmlNodeList stringElements = toastXml.GetElementsByTagName("text");
            stringElements[0].AppendChild(toastXml.CreateTextNode(title));
            stringElements[1].AppendChild(toastXml.CreateTextNode(message));

            if (string.IsNullOrEmpty(image) == false)
            {
                string imagePath = image;
                XmlNodeList imageElements = toastXml.GetElementsByTagName("image");
                imageElements[0].Attributes.GetNamedItem("src").NodeValue = imagePath;
            }

            ToastNotification toast = new ToastNotification(toastXml);

            ToastNotificationManager.CreateToastNotifier(appId).Show(toast);
        }


        public static Dictionary<ChatClassFlag, string> ChatClassDictionary = new Dictionary<ChatClassFlag, string>
        {
            { ChatClassFlag.World,"세계" },
            { ChatClassFlag.Team,"팀" },
            { ChatClassFlag.Recruit,"모집" },
            { ChatClassFlag.Guild,"길드" },
            { ChatClassFlag.Dojo,"협력" },


        };

        public static Dictionary<GuildPostEnum, string> GuildPostDictionary = new Dictionary<GuildPostEnum, string> {
            {GuildPostEnum.Member ,"회원"},
            {GuildPostEnum.Manager ,"간부"},
            {GuildPostEnum.Admin ,"부길드장"},
            {GuildPostEnum.Owner ,"길드장"},
        };

        public static Dictionary<string, string> TitleDic = new Dictionary<string, string>();

        public static Dictionary<string, string> AvatarFrameDic = new Dictionary<string, string>();

        public static Dictionary<string, string> AvatarDic = new Dictionary<string, string>();

        public static Dictionary<string, string> WeaponImageDic = new Dictionary<string, string>();


        public static string AvatarJsonPath = @"../../Resources/Database/AvatarConfigDataTable.json";
        public static string AvatarFrameJsonPath = @"../../Resources/Database/AvatarFrameConfigDataTable.json";
        public static string StaticWeaponJsonPath = @"../../Resources/Database/StaticWeaponDataTable.json";
        public static string TitleJsonPath = @"../../Resources/Database/DT_Title.json";
        public static string LocalJsonPath = @"../../Resources/Database/Game.json";

        public static Regex regex = new Regex("^\\/(.+\\/)*(.+)\\.(.+)$");

        public const string UserSettingsFileName = @"\Settings.ini";

        private static string filterString;
        public static string FilterString
        {
            get { return filterString; }
            set
            {
                filterString = value;
                SetiniValue("Setting", "FilterString", value);
            }
        }

        private static int ethernetIndex;
        public static int EthernetIndex
        {
            get { return ethernetIndex; }
            set
            {
                ethernetIndex = value;
                SetiniValue("Setting", "EthernetIndex", value.ToString());
            }
        }


        public static PacketSniffer Sniffer { get; set; }

        private static string GetiniValue(string section, string key)
        {
            var temp = new StringBuilder(255);
            GetPrivateProfileString(section, key, null, temp, 255, AppDomain.CurrentDomain.BaseDirectory + UserSettingsFileName);
            return temp.ToString();
        }

        private static void SetiniValue(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, AppDomain.CurrentDomain.BaseDirectory + UserSettingsFileName);
        }


        static Global()
        {

            filterString = GetiniValue("Setting", "FilterString");
            if (int.TryParse(GetiniValue("Setting", "EthernetIndex"), out int index))
            {
                ethernetIndex = index;
            }
            Sniffer = new PacketSniffer();

        }

        public static void Initlaize()
        {
            dynamic avatar = JsonConvert.DeserializeObject(File.ReadAllText(AvatarJsonPath, Encoding.UTF8));
            foreach (dynamic item in avatar[0].Rows)
            {
                AvatarDic.Add(item.Name.ToLower(), regex.Match(item.Value.BigImage.AssetPathName.ToString()).Groups[3].Value);
            }
            dynamic avatarFrame = JsonConvert.DeserializeObject(File.ReadAllText(AvatarFrameJsonPath, Encoding.UTF8));

            foreach (dynamic item in avatarFrame[0].Rows)
            {
                AvatarFrameDic.Add(item.Name.ToLower(), regex.Match(item.Value.BigImage.AssetPathName.ToString()).Groups[3].Value);
            }

            dynamic staticWeapon = JsonConvert.DeserializeObject(File.ReadAllText(StaticWeaponJsonPath, Encoding.UTF8));

            foreach (dynamic item in staticWeapon[0].Rows)
            {

                WeaponImageDic.Add(item.Name.ToLower(), regex.Match(item.Value.WeaponIconForMatrix.AssetPathName.ToString()).Groups[3].Value);
            }

            dynamic title = JsonConvert.DeserializeObject(File.ReadAllText(TitleJsonPath, Encoding.UTF8));

            dynamic titleLoc = JsonConvert.DeserializeObject(File.ReadAllText(LocalJsonPath, Encoding.UTF8));


            foreach (dynamic item in title[0].Rows)
            {
                if (item.Value.Name.TableId != null)
                {

                    if (item.Value.Name.LocalizedString != null)
                    {
                        TitleDic.Add(item.Name, item.Value.Name.LocalizedString);
                        continue;
                    }

                    dynamic tableId = regex.Match(item.Value.Name.TableId.ToString()).Groups[3].Value;
                    dynamic table = titleLoc[tableId.ToString()];
                    string na = table[item.Value.Name.Key.ToString()];

                    TitleDic.Add(item.Name, na);

                }
            }
        }
    }
}
