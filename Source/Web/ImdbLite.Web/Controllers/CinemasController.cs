namespace ImdbLite.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using ImdbLite.Data.UnitOfWork;

    using DbModel = ImdbLite.Data.Models.Cinema;
    using IndexViewModel = ImdbLite.Web.ViewModels.Cinemas.CinemasIndexViewModel;
    using QuickViewViewModel = ImdbLite.Web.ViewModels.Cinemas.CinemaQuickViewViewModel;
    using DetailsViewModel = ImdbLite.Web.ViewModels.Cinemas.CinemaDetailsViewModel;

    public class CinemasController : BaseEntityController
    {
        public CinemasController(IImdbLiteData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            var data = GetData<IndexViewModel>();

            return View(data.ToList());
        }

        [HttpGet]
        public ActionResult QuickView(int? id)
        {
            var model = base.GetViewModel<DbModel, QuickViewViewModel>(id);
            if (model == null)
            {
                model = GetData<QuickViewViewModel>().FirstOrDefault();
            }

            return PartialView("_CinemaQuickViewPartial", model);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            var model = base.GetViewModel<DbModel, DetailsViewModel>(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        protected override IQueryable<TViewModel> GetData<TViewModel>()
        {
            return this.Data.Cinemas.All().Project().To<TViewModel>();
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Cinemas.GetById(id) as T;
        }
    }
}