using KekboomKawaii.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KekboomKawaii.ViewModels
{


    public class UserChatViewModel : ViewModelBase
    {
        UserChat userChat;

        // public string DisplayedAvatar => @"\Resources\Avatar\aige.png";
        public string DisplayedAvatar => $@"pack://application:,,,/Resources/Avatar/{userChat.Avatar}.png";
        public string DisplayedAvatarFrame => $@"pack://application:,,,/Resources/AvatarFrame/{userChat.AvatarFrame}.png";
        public string DisplayedSuppressor => $@"pack://application:,,,/Resources/Suppressor/{userChat.SuppressorLevel}.png";
        public string DisplayedGender => $@"pack://application:,,,/Resources/Gender/{(userChat.Gender == GenderEnum.Female ? "Female" : "Male")}.png";
        public string NickName => userChat.NickName;
        public ChatClassEnum ChatClassState => userChat.ChatClass;
        public string DisplayedChannel => Global.ChatClassDictionary[userChat.ChatClass];
        public string DisplayUID => $"UID:{userChat.Server}{userChat.UID}";
        public string DisplayedLevel => $"Lv{userChat.Level}";

        public string DebugAvatar => userChat.Avatar;
        public string DebugAvatarFrame => userChat.AvatarFrame;


        public string DisplayedSticker
        {
            get
            {
                var stickerName = Regex.Match(userChat.Message, @"(?<=#1#big)(\d+)(?=#)").Value;
                if (stickerName != string.Empty)
                {
                    return $@"pack://application:,,,/Resources/Sticker/{stickerName}.gif";
                }
                return null;
            }
        }
        public string Title
        {
            get
            {
                if (Global.TitleDictionary.TryGetValue(userChat.Title, out string title))
                {
                    return title;
                }
                return userChat.Title;
            }
        }
        public string Violation => userChat.Violation;
        public string Message
        {
            get
            {
                var stickerName = Regex.Match(userChat.Message, @"(?<=#1#big)(\d+)(?=#)").Value;
                if (stickerName == string.Empty)
                {
                    return Regex.Replace(userChat.Message, "<[^>]*>", "");
                }
                return string.Empty;
            }
        }


        private DateTime chatTime;
        public DateTime ChatTime
        {
            get { return chatTime; }
            set { chatTime = value; OnPropertyChanged(); }
        }

        public UserChatViewModel()
        {

        }

        public UserChatViewModel(UserChat userChat)
        {
            this.userChat = userChat;
            ChatTime = DateTime.Now;

            var filters = Global.FilterString.Split(';');

            if (filters.Any(str => userChat.Message.Contains(str)))
            {
                Global.ShowToast("KekboomKawaii", userChat.NickName, Message, string.Empty);
                SystemSounds.Beep.Play();
            }
        }
    }
}
