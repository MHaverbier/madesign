using System.Runtime.InteropServices;
using System.Windows;

namespace it.HLClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
#if DEBUG
            var ui = new HL();
            ui.Show();
#endif
        }
    }
}
