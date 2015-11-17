using System;
using System.Threading;

namespace StockTicker.Library.TimerProvider
{
	public interface ITimerProvider
	{
		void Register(TimerCallback callback, TimeSpan interval);
	}
}