using PriceChecker.Common;
using PriceChecker.Logic;
using PriceChecker.UI.View;
using PriceChecker.UI.ViewModel;
using System;
using System.IO;

namespace PriceChecker.UI
{
    /// <summary>
    /// Main handler class
    /// </summary>
    internal class MainHandler
    {
        /// <summary>
        /// Name of log file to which logs are added.
        /// </summary>
        public string LogFileName { get; set; } = "Log.txt";

        /// <summary>
        /// Start the application
        /// </summary>
        public void Start()
        {
            Logger.AddAction(LogAction);
            Logger.Log("Application Started");
            int numberOfDays = 30;

            IRateCollector rateCollector = GetDataSource();
            rateCollector.DayCount = numberOfDays;

            var mainViewModel = new MainViewModel();
            mainViewModel.RateCollector = rateCollector;
            mainViewModel.StartDate = DateTime.Now.AddDays(-numberOfDays);
            mainViewModel.EndDate = DateTime.Now;

            var mainWindow = new MainWindow();
            mainWindow.DataContext = mainViewModel;

            mainWindow.Show();

            StartAutoUpdate(mainViewModel);
        }

        /// <summary>
        /// Create instance of IRateCollector which provides data model
        /// </summary>
        /// <returns>Instance of IRateCollector</returns>
        private IRateCollector GetDataSource()
        {
            // isRealData = True  : Real data from the website will be read and displayed
            // isRealData = False : Random sample data will be displayed
            bool isRealData = false;

            IRateCollector rateCollector = Factory.GetRateCollector(isRealData);
            rateCollector.Symbol1 = "BTC";
            rateCollector.Symbol2 = "USD";
            return rateCollector;
        }

        /// <summary>
        /// Start automatic updating of data table on given interval
        /// </summary>
        /// <param name="mainViewModel">Instance of MainViewModel</param>
        private void StartAutoUpdate(MainViewModel mainViewModel)
        {
            int dueTime = 200; // Start after 200 Milli seconds
            int interval = 1000 * 60 * 60 * 24; // Update in every 24 hours
            mainViewModel.StartAutoUpdate(dueTime, interval);
        }

        /// <summary>
        /// Log method to be called on each log
        /// </summary>
        /// <param name="message">String message to be logged</param>
        private void LogAction(string message)
        {
            try
            {
                var time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss > ");
                Console.WriteLine(message);
                File.AppendAllText(LogFileName, "\r\n" + time + message);
            }
            catch { }
        }
    }
}
