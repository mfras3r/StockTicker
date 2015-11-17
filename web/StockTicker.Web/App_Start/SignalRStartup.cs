using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin;
using Microsoft.Practices.Unity;
using Owin;
using StockTicker.Library.StockTicker;
using StockTicker.Library.TimerProvider;
using StockTicker.Web;

[assembly: OwinStartup(typeof(SignalRStartup))]

namespace StockTicker.Web
{
	public class SignalRStartup
	{
		public void Configuration(IAppBuilder app)
		{
			// For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
			var unityContainer = new UnityContainer();
			var unityHubActivator = new MvcHubActivator(unityContainer);
			
			unityContainer
				.RegisterType<IStockTickerConfig, StockTickerConfig>(new ContainerControlledLifetimeManager());
			unityContainer.RegisterType<ITimerProvider, TimerProvider>();
			unityContainer
				.RegisterType<IStockTickerService, StockTickerService>(new ContainerControlledLifetimeManager());

			GlobalHost.DependencyResolver
				.Register(typeof(IHubActivator), () => unityHubActivator);

			app.MapSignalR();
		}
	}
}
