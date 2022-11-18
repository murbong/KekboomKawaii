using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KekboomKawaii.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Property

        private bool windowCondition;
        public bool WindowCondition
        {
            get { return windowCondition; }
            set
            {
                windowCondition = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Command
        private RelayCommand windowCloseCommand;
        public ICommand WindowCloseCommand => windowCloseCommand ?? (windowCloseCommand = new RelayCommand(WindowClose));

        private RelayCommand windowMaximizeCommand;
        public ICommand WindowMaximizeCommand => windowMaximizeCommand ?? (windowMaximizeCommand = new RelayCommand(WindowMaximize));

        private RelayCommand windowMinimizeCommand;
        public ICommand WindowMinimizeCommand => windowMinimizeCommand ?? (windowMinimizeCommand = new RelayCommand(WindowMinimize));

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindowViewModel()
        {
            WindowCondition = false;
        }

        #region Methods

        public void WindowClose(object sender)
        {
            Application.Current.MainWindow.Close();
            Environment.Exit(0);
        }
        public void WindowMaximize(object sender)
        {
            if (!WindowCondition)
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            else
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            WindowCondition ^= true;
        }
        public void WindowMinimize(object sender)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        #endregion
    }
}
