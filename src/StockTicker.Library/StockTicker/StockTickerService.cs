using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using StockTicker.Library.Extensions;
using StockTicker.Library.TimerProvider;

namespace StockTicker.Library.StockTicker
{
	public class StockTickerService : IStockTickerService
	{
		public StockTickerService(IStockTickerConfig config, ITimerProvider timer)
		{
			Stocks = new ConcurrentDictionary<string, Stock>();
			UpdateStockPricesLock = new object();

			RangePercent = config.RangePercent;
			UpdateInterval = config.UpdateInterval;
			
			config.InitialStocks.ForEach(s => Stocks.TryAdd(s.Symbol, s));
			timer.Register(UpdateStockPrices, UpdateInterval);
		}

		protected ConcurrentDictionary<string, Stock> Stocks { get; private set; }

		protected object UpdateStockPricesLock { get; private set; }

		#region Configuration
		protected double RangePercent { get; private set; }

		protected TimeSpan UpdateInterval { get; private set; }
		#endregion Configuration

		private volatile bool _updatingStockPrices;
		private readonly Random _updateOrNotRandom = new Random();

		public IEnumerable<Stock> GetAllStocks()
		{
			return Stocks.Values;
		}

		private void UpdateStockPrices(object state)
		{
			lock (UpdateStockPricesLock)
			{
				if (!_updatingStockPrices)
				{
					_updatingStockPrices = true;

					Stocks.Values
						.Where(TryUpdateStockPrice)
						.ForEach(BroadcastStockPrice);
					
					_updatingStockPrices = false;
				}
			}
		}

		private bool TryUpdateStockPrice(Stock stock)
		{
			var r = _updateOrNotRandom.NextDouble();
			if (r > .1)
			{
				return false;
			}

			var random = new Random((int)Math.Floor(stock.Price));
			var percentChange = random.NextDouble()*RangePercent;
			var change = Math.Round(stock.Price*(decimal) percentChange, 2);
			change = random.NextDouble() > .51 ? change : -change;

			stock.Price += change;
			return true;
		}

		public event EventHandler<Stock> StockUpdated;
		private void BroadcastStockPrice(Stock stock)
		{
			if (StockUpdated != null)
				StockUpdated(this, stock);
		}
	}
}