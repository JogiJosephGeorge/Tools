using System.Windows;

namespace PriceChecker.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Instance of MainHandler
        /// </summary>
        private MainHandler mainHandler = new MainHandler();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            mainHandler.Start();
        }
    }
}
