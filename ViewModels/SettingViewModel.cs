using PcapDotNet.Core;
using System;
using System.Collections.Generic;
using System.Media;
using System.Reflection.Emit;
using System.Windows.Controls;
using System.Windows.Input;

namespace KekboomKawaii.ViewModels
{
    public class SettingViewModel : ViewModelBase
    {

        private RelayCommand outFocusCommand;

        public ICommand OutFocusCommand => outFocusCommand ?? (outFocusCommand = new RelayCommand(OutFocus));


        public string FilterString
        {
            get { return Global.FilterString; }
            set
            {
                Global.FilterString = value;
                OnPropertyChanged();
            }
        }


        public int SelectedDeviceIndex
        {
            get { return Global.EthernetIndex; }
            set
            {
                Global.EthernetIndex = value;
                OnPropertyChanged();
            }
        }

        private bool isPacketTaskRunning;
        public bool IsPacketTaskRunning
        {
            get { return isPacketTaskRunning; }
            set
            {
                isPacketTaskRunning = value;
                SystemSounds.Beep.Play();
                Global.Sniffer.Stop();
                if (isPacketTaskRunning)
                {
                    Global.Sniffer.Select(Global.EthernetIndex);
                    Global.Sniffer.RunAsync();
                }
                OnPropertyChanged();
            }
        }
        public bool SelectWorld
        {
            get
            {
                return Global.ChatClassFilter.HasFlag(ChatClassFlag.World);
            }
            set
            {
                Global.ChatClassFilter = value ? Global.ChatClassFilter | ChatClassFlag.World : Global.ChatClassFilter & ~ChatClassFlag.World;
                OnPropertyChanged();
            }
        }

        public bool SelectGuild
        {
            get
            {
                return Global.ChatClassFilter.HasFlag(ChatClassFlag.Guild);
            }
            set
            {
                Global.ChatClassFilter = value ? Global.ChatClassFilter | ChatClassFlag.Guild : Global.ChatClassFilter & ~ChatClassFlag.Guild;
                OnPropertyChanged();
            }
        }

        public IList<LivePacketDevice> AllLocalMachine { get { return Global.Sniffer.AllDevices; } }

        public SettingViewModel()
        {
            IsPacketTaskRunning = true;
        }

        public void OutFocus(object sender)
        {
            var grid = sender as Grid;
            grid.Focus();

        }
    }
}
