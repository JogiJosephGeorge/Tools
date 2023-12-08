using PriceChecker.Common;

namespace PriceChecker.Logic.TestData
{
    /// <summary>
    /// This class Implements IRateCollector for testing purpose only.
    /// It provides only random data to verify the UI.
    /// </summary>
    internal class RateCollectorRandomValues : IRateCollector
    {
        /// <summary>
        /// Instance of Random class to provide random sample values
        /// </summary>
        Random random = new Random();

        /// <summary>
        /// First symbol for which stock info to be collected
        /// </summary>
        public string Symbol1 { get; set; } = string.Empty;

        /// <summary>
        /// Second symbol for which stock info to be collected
        /// </summary>
        public string Symbol2 { get; set; } = string.Empty;

        /// <summary>
        /// Number of days to be considered while taking history
        /// </summary>
        public int DayCount { get; set; } = 0;

        /// <summary>
        /// Get stock data history as random values.
        /// </summary>
        /// <returns>The stock data history as random values.</returns>
        public List<StockData> GetHistory()
        {
            var history = new List<StockData>();
            var date = DateTime.Today;
            for (int i = 0; i < DayCount; i++)
            {
                var stockData = new StockData();
                stockData.Date = new DateOnly(date.Year, date.Month, date.Day);
                stockData.Value = GetRandomValue();
                history.Insert(0, stockData);

                date = date.AddDays(-1);
            }

            return history;
        }

        /// <summary>
        /// Get average price of stock as random values.
        /// </summary>
        /// <param name="start">Start date does not have any significance in this implementation.</param>
        /// <param name="end">End date does not have any significance in this implementation.</param>
        /// <returns>The average price of stock as random values.</returns>
        public double GetAveragePrice(DateTime start, DateTime end)
        {
            return GetRandomValue();
        }

        /// <summary>
        /// Generate a random value
        /// </summary>
        /// <returns>A new random value.</returns>
        private double GetRandomValue()
        {
            int randomBegin = 2000;
            int randomEnd = 3500;
            return random.Next(randomBegin, randomEnd);
        }
    }
}
