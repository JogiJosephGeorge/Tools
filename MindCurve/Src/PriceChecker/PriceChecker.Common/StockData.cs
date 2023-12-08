namespace PriceChecker.Common
{
    /// <summary>
    /// Model class for Stock data on a date
    /// </summary>
    public class StockData
    {
        /// <summary>
        /// Date of stock data
        /// </summary>
        public DateOnly Date { get; set; }

        /// <summary>
        /// Value of the stock data
        /// </summary>
        public double Value { get; set; }
    }
}
