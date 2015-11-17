using System.Web.Mvc;

namespace StockTicker.Web.Areas.Web.Controllers
{
	public class DefaultController : Controller
    {
        // GET: Web/Default
        public ActionResult Index()
        {
            return View("~/Areas/Web/Views/Default/Index.cshtml");
        }
    }
}