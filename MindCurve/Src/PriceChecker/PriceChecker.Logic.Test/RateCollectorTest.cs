using PriceChecker.Logic.WebService;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PriceChecker.Logic.Test
{
    [TestClass]
    public class RateCollectorTest
    {
        [TestMethod]
        public void GetAveragePrice()
        {
            var rateCollector = Factory.GetRateCollector(false); // TODO: Use CexRateCollector
            rateCollector.Symbol1 = "BTC";
            rateCollector.Symbol2 = "USD";
            rateCollector.DayCount = 5;

            var start = new DateTime(2023, 12, 1);
            var end = new DateTime(2023, 12, 7);
            double avgPrice = rateCollector.GetAveragePrice(start, end);

            Assert.AreEqual(3000, avgPrice); // TODO: Provide the actual average price
        }

        [TestMethod]
        public void GetHistory()
        {
            var rateCollector = Factory.GetRateCollector(false);
            rateCollector.Symbol1 = "BTC";
            rateCollector.Symbol2 = "USD";
            rateCollector.DayCount = 5;

            var history = rateCollector.GetHistory();

            Assert.AreEqual(5, history.Count);
        }
    }
}
