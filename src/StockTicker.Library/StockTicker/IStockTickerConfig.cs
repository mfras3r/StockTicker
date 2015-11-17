using System;
using System.Collections.Generic;

namespace StockTicker.Library.StockTicker
{
	public interface IStockTickerConfig
	{
		double RangePercent { get; set; }
		TimeSpan UpdateInterval { get; set; }
		IList<Stock> InitialStocks { get; set; }
	}
}