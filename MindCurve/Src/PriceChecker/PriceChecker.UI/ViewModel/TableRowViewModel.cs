using PriceChecker.Common;

namespace PriceChecker.UI.ViewModel
{
    /// <summary>
    /// View model class for table row
    /// </summary>
    class TableRowViewModel
    {
        /// <summary>
        /// Instance of StockData
        /// </summary>
        private StockData stockData;

        /// <summary>
        /// Create instance of TableRowViewModel
        /// </summary>
        /// <param name="stockData">Instance of StockData</param>
        public TableRowViewModel(StockData stockData)
        {
            this.stockData = stockData;
        }

        /// <summary>
        /// Date value representation of Stock data
        /// </summary>
        public string Date { get { return stockData.Date.ToString(); } }

        /// <summary>
        /// Price of Stock data
        /// </summary>
        public double Price { get { return stockData.Value; } }
    }
}
