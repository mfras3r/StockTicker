using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTicker.Library.StockTicker;
using StockTicker.Library.TimerProvider;

namespace StockTicker.Tests.StockTicker
{
	[TestClass]
	public class StockTickerServiceTests
	{
		public IStockTickerService UnitUnderTest { get; set; }

		public StockTickerConfig InitializeConfig(Dictionary<string, decimal> pricesDictionary)
		{
			return new StockTickerConfig()
			{
				InitialStocks = pricesDictionary
					.Select(kvp => new Stock {Symbol = kvp.Key, Price = kvp.Value})
					.ToList()
			};
		}

		[TestMethod]
		public void GetAllStocks_InitialState_DayOpenMatchesPrice()
		{
			// Arrange
			var stocks = new Dictionary<string, decimal>
			{
				{"MSFT", 35.17m},
				{"APPL", 44.55m},
				{"GOOG", 25.57m}
			};
			var config = InitializeConfig(stocks);
			var timerProvider = new ManualTimerProvider();
			UnitUnderTest = new StockTickerService(config, timerProvider);

			// Act
			var result = UnitUnderTest.GetAllStocks();
			
			// Assert
			Assert.AreEqual(3, result.Count());
			var msft = result.First(s => s.Symbol == "MSFT");
			Assert.AreEqual(stocks["MSFT"], msft.DayOpen);
			var appl = result.First(s => s.Symbol == "APPL");
			Assert.AreEqual(stocks["APPL"], appl.DayOpen);
			var goog = result.First(s => s.Symbol == "GOOG");
			Assert.AreEqual(stocks["GOOG"], goog.DayOpen);
		}
	}
}
