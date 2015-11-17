using System;
using System.Collections.Generic;

namespace StockTicker.Library.StockTicker
{
	public interface IStockTickerService
	{
		IEnumerable<Stock> GetAllStocks();
		event EventHandler<Stock> StockUpdated;
	}
}