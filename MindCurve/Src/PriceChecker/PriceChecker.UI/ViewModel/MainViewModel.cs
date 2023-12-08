using PriceChecker.Common;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace PriceChecker.UI.ViewModel
{
    /// <summary>
    /// Main view model class
    /// </summary>
    class MainViewModel : ViewModelBase, IDataErrorInfo
    {
        /// <summary>
        /// Instance of TimerExt
        /// </summary>
        private TimerExt? timer = null;

        /// <summary>
        /// Last updated date and time
        /// </summary>
        private string _lastUpdated = string.Empty;

        /// <summary>
        /// Average price to be displayed
        /// </summary>
        private double _avgPrice = 0;

        /// <summary>
        /// Create instance of MainViewModel
        /// </summary>
        public MainViewModel()
        {
            CalculateAvgPriceCommand = new CommandHandler(CalculateAvgPrice);
            UpdateCommand = new CommandHandler(Update);
        }

        /// <summary>
        /// Instance of IRateCollector which is the data source provider
        /// </summary>
        public IRateCollector? RateCollector { get; set; } = null;

        /// <summary>
        /// Table data bound with UI
        /// </summary>
        public ObservableCollection<TableRowViewModel> TableData { get; set; }
            = new ObservableCollection<TableRowViewModel>();

        /// <summary>
        /// Last updated date and time
        /// </summary>
        public string LastUpdated
        {
            get
            {
                return _lastUpdated;
            }
            set
            {
                _lastUpdated = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Average price to be displayed
        /// </summary>
        public double AvgPrice
        {
            get
            {
                return _avgPrice;
            }
            set
            {
                _avgPrice = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Start Date
        /// </summary>
        private DateTime startDate;

        /// <summary>
        /// End Date
        /// </summary>
        private DateTime endDate;

        /// <summary>
        /// Start Date
        /// </summary>
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsCalculateEnabled));
            }
        }

        /// <summary>
        /// End Date
        /// </summary>
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                endDate = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsCalculateEnabled));
            }
        }

        /// <summary>
        /// Specifies to enable calculate button or not.
        /// </summary>
        public bool IsCalculateEnabled
        {
            get
            {
                if (EndDate > DateTime.Now)
                {
                    return false;
                }

                if (StartDate > EndDate)
                {
                    return false;
                }

                return true;
            }
        }

        #region IDataErrorInfo

        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(StartDate))
                {
                    if (StartDate > DateTime.Now)
                    {
                        return "Start date can not be after today.";
                    }
                }
                else if (columnName == nameof(EndDate))
                {
                    if (EndDate > DateTime.Now)
                    {
                        return "End date can not be after today.";
                    }
                }

                return string.Empty;
            }
        }

        public string Error => throw new NotImplementedException();

        #endregion

        /// <summary>
        /// Command property bind to Calculate Average Price button
        /// </summary>
        public ICommand CalculateAvgPriceCommand { get; private set; }

        /// <summary>
        /// Command property bind to Update button
        /// </summary>
        public ICommand UpdateCommand { get; private set; }

        /// <summary>
        /// Start automatic updating of data table on given interval
        /// </summary>
        /// <param name="dueTime">Waiting time to call the action for first time.</param>
        /// <param name="period">Waiting time between two calls.</param>
        public void StartAutoUpdate(int dueTime, int interval)
        {
            timer = new TimerExt(Update, dueTime, interval);
        }

        /// <summary>
        /// Method to be called on clicking button : Calculate Average Price
        /// </summary>
        private void CalculateAvgPrice()
        {
            if (RateCollector == null)
            {
                return;
            }

            AvgPrice = RateCollector.GetAveragePrice(StartDate, EndDate);
            Logger.Log("Average price calculated.");
        }

        /// <summary>
        /// Method to be called on clicking button : Update
        /// </summary>
        private void Update()
        {
            if (RateCollector == null)
            {
                return;
            }

            var history = RateCollector.GetHistory();
            if (history == null)
            {
                return;
            }

            TableData.Clear();
            history.ForEach(item => TableData.Add(new TableRowViewModel(item)));

            LastUpdated = "Last updated on " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            OnPropertyChanged(nameof(TableData));
            Logger.Log("Updated Table.");
        }
    }
}
