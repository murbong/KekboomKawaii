using KekboomKawaii.ViewModels;
using System.Windows.Controls;

namespace KekboomKawaii.Views
{
    /// <summary>
    /// PlayerListView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PlayerListView : UserControl
    {
        public PlayerListView()
        {
            InitializeComponent();
            DataContext = new PlayerListViewModel();
        }
    }
}
