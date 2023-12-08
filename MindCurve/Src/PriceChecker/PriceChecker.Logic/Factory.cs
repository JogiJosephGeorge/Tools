using PriceChecker.Common;
using PriceChecker.Logic.TestData;
using PriceChecker.Logic.WebService;

namespace PriceChecker.Logic
{
    /// <summary>
    /// Factory class to create instance of actual implementations.
    /// </summary>
    public class Factory
    {
        /// <summary>
        /// Get new instance of IRateCollector based on given input
        /// </summary>
        /// <param name="isRealData">Specifies the real instance / sample data provider is created.</param>
        /// <returns>new instance of IRateCollector based on given input</returns>
        public static IRateCollector GetRateCollector(bool isRealData)
        {
            if (isRealData)
            {
                return new CexRateCollector();
            }

            return new RateCollectorRandomValues();
        }
    }
}
