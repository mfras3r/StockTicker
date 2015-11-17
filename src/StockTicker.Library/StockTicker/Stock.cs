using System;

namespace StockTicker.Library.StockTicker
{
	public class Stock
	{
		public string Symbol { get; set; }

		public decimal DayOpen { get; set; }

		private decimal _price;
		public decimal Price
		{
			get
			{
				return _price;
			}
			set
			{
				if (_price == value)
				{
					return;
				}

				_price = value;

				if (DayOpen == 0)
				{
					DayOpen = _price;
				}
			}
		}

		public decimal Change
		{
			get { return Price - DayOpen; }
		}

		public double PercentChange
		{
			get { return (double) Math.Round(Change/Price, 4); }
		}
	}
}