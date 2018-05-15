namespace ImdbLite.Web.Controllers
{
    using System.Web.Mvc;

    using ImdbLite.Data.UnitOfWork;

    public class HomeController : BaseController
    {
        public HomeController(IImdbLiteData data)
            : base(data) { }

        public ActionResult Index()
        {
            return View();
        }
    }
}