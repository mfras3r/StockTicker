using System.Web.Mvc;
using System.Web.Routing;

namespace StockTicker.Web.App_Start
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "Home Page",
				url: "",
				defaults: new { controller = "Default", action = "Index", area = "Web" }
			);
			routes.MapRoute(
				name: "Angular",
				url: "Angular",
				defaults: new {controller = "Default", action = "Angular", area = "Web"}
			);
		}
	}
}