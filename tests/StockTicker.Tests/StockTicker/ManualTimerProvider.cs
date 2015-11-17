using System;
using System.Threading;
using StockTicker.Library.TimerProvider;

namespace StockTicker.Tests.StockTicker
{
	public class ManualTimerProvider : ITimerProvider
	{
		public void Register(TimerCallback callback, TimeSpan interval)
		{
			Callback = callback;
		}

		protected TimerCallback Callback { get; private set; }

		public void Tick()
		{
			Callback(null);
		}
	}
}
