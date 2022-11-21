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
        public PlayerViewModel SelectedPlayerViewModel { get { return selectedPlayerViewModel; } set { selectedPlayerViewModel = value; OnPropertyChanged(); } }
        public PlayerListViewModel()
        {
            Global.Sniffer.PlayerDataEvent += Sniffer_PayloadEvent;
        }

        private void Sniffer_PayloadEvent(PlayerData data)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var items = this.Where(e => e.UID == data.UID).ToArray();
                for (var i = 0; i < items.Length; i++)
                {
                    Remove(items[i]);
                }
                Add(new PlayerViewModel(data));
            });
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
    }
}
