using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Practices.Unity;

namespace StockTicker.Web
{
	public class MvcHubActivator : IHubActivator
	{
		public MvcHubActivator(IUnityContainer container)
		{
			_container = container;
		}

		private readonly IUnityContainer _container;

		public IHub Create(HubDescriptor descriptor)
		{
			return (IHub) _container.Resolve(descriptor.HubType);
		}
	}
}