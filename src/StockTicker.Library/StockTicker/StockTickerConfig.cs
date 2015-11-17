using System;
using System.Collections.Generic;
using System.Configuration;

namespace StockTicker.Library.StockTicker
{
	public class StockTickerConfig : IStockTickerConfig
	{
		public StockTickerConfig()
		{
			var rangePercentConfig = ConfigurationManager.AppSettings["StockTicker:RangePercent"];
			double rangePercent;
			RangePercent = double.TryParse(rangePercentConfig, out rangePercent)
				? rangePercent
				: .002;

			var updateIntervalConfig = ConfigurationManager.AppSettings["StockTicker:UpdateInterval"];
			double updateInterval;
			UpdateInterval = double.TryParse(updateIntervalConfig, out updateInterval)
				? TimeSpan.FromMilliseconds(updateInterval)
				: TimeSpan.FromMilliseconds(250);
			
			InitialStocks = new List<Stock>
			{
				new Stock{Symbol = "MSFT", Price = 30.31m},
				new Stock{Symbol = "APPL", Price = 578.18m},
				new Stock{Symbol = "GOOG", Price = 570.30m}
			};
		}

		public double RangePercent { get; set; }
		public TimeSpan UpdateInterval { get; set; }

		public IList<Stock> InitialStocks { get; set; } 
	}
}