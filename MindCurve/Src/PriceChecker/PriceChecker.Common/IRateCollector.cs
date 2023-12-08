namespace PriceChecker.Common
{
    /// <summary>
    /// Common interface for rate collector
    /// </summary>
    public interface IRateCollector
    {
        /// <summary>
        /// First symbol for which stock info to be collected
        /// </summary>
        string Symbol1 { get; set; }

        /// <summary>
        /// Second symbol for which stock info to be collected
        /// </summary>
        string Symbol2 { get; set; }

        /// <summary>
        /// Number of days to be considered while taking history
        /// </summary>
        int DayCount { get; set; }

        /// <summary>
        /// Get stock data history for given number of days.
        /// </summary>
        /// <returns>The stock data history for given number of days.</returns>
        List<StockData> GetHistory();

        /// <summary>
        /// Get average price of stock between given two days.
        /// </summary>
        /// <param name="start">Start date of range of days on which price calculated.</param>
        /// <param name="end">End date of range of days on which price calculated.</param>
        /// <returns>The average price of stock between given two days.</returns>
        double GetAveragePrice(DateTime start, DateTime end);
    }
}
