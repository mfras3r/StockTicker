using System;
using System.Collections.Generic;

namespace StockTicker.Library.Extensions
{
	public static class IEnumerableExtensions
	{
		public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> doAction)
		{
			foreach (var item in enumerable)
			{
				doAction(item);
			}
		}
	}
}