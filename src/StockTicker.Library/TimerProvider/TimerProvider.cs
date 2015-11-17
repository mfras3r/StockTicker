using System;
using System.Threading;

namespace StockTicker.Library.TimerProvider
{
	public class TimerProvider : ITimerProvider
	{
		public Timer Timer { get; set; }

		public void Register(TimerCallback callback, TimeSpan interval)
		{
			Timer = new Timer(callback, null, interval, interval);
		}
	}
}
