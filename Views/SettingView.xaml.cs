
using KekboomKawaii.ViewModels;
using System.Windows.Controls;
namespace KekboomKawaii.Views
{
    /// <summary>
    /// SettingView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SettingView : UserControl
    {
        public SettingView()
        {
            InitializeComponent();
            DataContext = new SettingViewModel();
        }

    }
}
