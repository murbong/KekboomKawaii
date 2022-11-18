
using System.Windows;
using System.Windows.Controls;

using KekboomKawaii.ViewModels;

namespace KekboomKawaii.Views
{
    /// <summary>
    /// ChattingView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ChattingView : UserControl
    {

        public ChattingView()
        {
            InitializeComponent();
            DataContext = new ChattingViewModel();
        }
    }
}
