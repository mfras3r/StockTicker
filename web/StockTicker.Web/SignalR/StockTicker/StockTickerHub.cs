using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using StockTicker.Library.StockTicker;

namespace StockTicker.Web.SignalR.StockTicker
{
	[HubName("stockTicker")]
	public class StockTickerHub : Hub
	{
		private readonly IStockTickerService _stockTickerHub;

		public StockTickerHub(IStockTickerService stockTicker)
		{
			_stockTickerHub = stockTicker;
			stockTicker.StockUpdated += UpdateStockPrice;
		}

		public IEnumerable<Stock> GetAllStocks()
		{
			return _stockTickerHub.GetAllStocks();
		}

		public void UpdateStockPrice(object ticker, Stock stock)
		{
			Clients.All.updateStockPrice(stock);
		}
	}
}