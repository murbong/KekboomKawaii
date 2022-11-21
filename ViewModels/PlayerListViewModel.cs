using KekboomKawaii.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KekboomKawaii.ViewModels
{
    public class PlayerListViewModel : ObservableCollection<PlayerViewModel>
    {

        private PlayerViewModel selectedPlayerViewModel;
        public PlayerViewModel SelectedPlayerViewModel { get { return selectedPlayerViewModel; } set { selectedPlayerViewModel = value;OnPropertyChanged(); } }
        public PlayerListViewModel()
        {
            Global.Sniffer.PlayerDataEvent += Sniffer_PayloadEvent;
        }

        private void Sniffer_PayloadEvent(PlayerData userChat)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Add(new PlayerViewModel(userChat));
            });
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
    }
}
