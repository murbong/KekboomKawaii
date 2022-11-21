using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KekboomKawaii.Models;
using System.Windows.Threading;
using System.Windows;
using System.Runtime.CompilerServices;
using System.Media;
using Windows.System;

namespace KekboomKawaii.ViewModels
{

    public class ChattingViewModel : ObservableCollection<UserChatViewModel>
    {

        private bool selectAll;
        public bool SelectAll
        {
            get
            {
                return selectAll;
            }
            set
            {
                selectAll = value; OnPropertyChanged();
                OnPropertyChanged("VisibleCollection");
                SelectWorld = SelectGuild = SelectTeam = SelectRecruit = selectAll;
            }
        }

        private bool selectWorld;
        public bool SelectWorld
        {
            get
            {
                return selectWorld;
            }
            set
            {
                selectWorld = value; OnPropertyChanged();
                SetSelectAllState();

            }
        }

        private bool selectGuild;
        public bool SelectGuild
        {
            get
            {
                return selectGuild;
            }
            set
            {
                selectGuild = value; OnPropertyChanged();
                SetSelectAllState();

            }
        }

        private bool selectTeam;
        public bool SelectTeam
        {
            get
            {
                return selectTeam;
            }
            set
            {
                selectTeam = value; OnPropertyChanged();
                SetSelectAllState();

            }
        }


        private bool selectRecruit;
        public bool SelectRecruit
        {
            get
            {
                return selectRecruit;
            }
            set
            {
                selectRecruit = value; OnPropertyChanged();
                SetSelectAllState();

            }
        }

        public void SetSelectAllState()
        {
            selectAll = selectWorld && selectRecruit && selectGuild && selectTeam;
            OnPropertyChanged("SelectAll");
            OnPropertyChanged("VisibleCollection");
        }

        public ChatClassEnum CurrentVisibleChat
        {
            get
            {
                ChatClassEnum chatClass = ChatClassEnum.None;
                if (selectGuild) chatClass |= ChatClassEnum.Guild;
                if (selectRecruit) chatClass |= ChatClassEnum.Recruit;
                if (selectTeam) chatClass |= ChatClassEnum.Team;
                if (selectWorld) chatClass |= ChatClassEnum.World;
                return chatClass;
            }
        }


        public IEnumerable<UserChatViewModel> VisibleCollection => Items.Where(e => CurrentVisibleChat.HasFlag(e.ChatClassState));
        public UserChatViewModel LastChatViewModel { get { return Count > 0 ? this[Count - 1] : null; } }

        public ChattingViewModel()
        {
            Global.Sniffer.UserChatEvent += Sniffer_PayloadEvent;
            SelectAll = true;
        }

        private void Sniffer_PayloadEvent(UserChat userChat)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Add(new UserChatViewModel(userChat));
                OnPropertyChanged("VisibleCollection");
                OnPropertyChanged("LastChatViewModel");
            });
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
    }
}
