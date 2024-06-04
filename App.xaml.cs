using System.Windows;

namespace KekboomKawaii
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Global.ImageInit();

            Global.Initlaize();
        }
    }
}
