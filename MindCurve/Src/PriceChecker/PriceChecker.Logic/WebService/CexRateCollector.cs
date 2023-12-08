using CoinbaseProApi.NetCore.Entities;
using Newtonsoft.Json.Linq;
using PriceChecker.Common;

namespace PriceChecker.Logic.WebService
{
    /// <summary>
    /// Implementation of IRateCollector from CEX.IO API
    /// </summary>
    internal class CexRateCollector : IRateCollector
    {
        /// <summary>
        /// Instance of HttpHelper
        /// </summary>
        private HttpHelper httpHelper = new HttpHelper();

        /// <summary>
        /// Base path of URL
        /// </summary>
        protected string UrlBasePath { get { return "https://cex.io/api/ohlcv/hd"; } }

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
        /// Get stock data history for given number of days.
        /// </summary>
        /// <returns>The stock data history for given number of days.</returns>
        public List<StockData> GetHistory()
        {
            var end = DateTime.UtcNow;
            var start = end.AddDays(-DayCount);
            return GetHistory(start, end);
        }

        /// <summary>
        /// Get average price of stock between given two days.
        /// </summary>
        /// <param name="start">Start date of range of days on which price calculated.</param>
        /// <param name="end">End date of range of days on which price calculated.</param>
        /// <returns>The average price of stock between given two days.</returns>
        public double GetAveragePrice(DateTime start, DateTime end)
        {
            var history = GetHistory(start, end);
            return history.Average(item => item.Value);
        }

        /// <summary>
        /// Get URL string for given range
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns>URL string for given range</returns>
        protected virtual string GetUrlForDateRange(DateTime startDate, DateTime endDate)
        {
            var start = ToString(startDate);
            var end = ToString(endDate);
            string parameters = $"?from={start}&to={end}";

            string apiUrl = $"{UrlBasePath}/{Symbol1}/{Symbol2}/{parameters}";
            return apiUrl;
        }

        /// <summary>
        /// Convert DateTime to URL format
        /// </summary>
        /// <param name="dateTime">Instance of DateTime to be converted to string.</param>
        /// <returns>String format of given DateTime</returns>
        protected string ToString(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }

        /// <summary>
        /// Get history of stock data on given range of dates
        /// </summary>
        /// <param name="start">Start date</param>
        /// <param name="end">End date</param>
        /// <returns>history of stock data on given range of dates</returns>
        private List<StockData> GetHistory(DateTime start, DateTime end)
        {
            string apiUrl = GetUrlForDateRange(start, end);

            string responseBody = httpHelper.Get(apiUrl);

            JObject jsonResult = JObject.Parse(responseBody);
            var data = jsonResult["data"];

            var history = new List<StockData>();
            if (data is not JArray dataArray)
            {
                return history;
            }

            foreach (var dataRow in dataArray)
            {
                double price = (double)dataRow[4];
                history.Add(new StockData
                {
                    Value = price,
                });
            }

            return history;
        }

    }
}
