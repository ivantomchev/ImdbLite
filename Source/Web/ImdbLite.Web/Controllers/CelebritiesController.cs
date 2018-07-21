namespace ImdbLite.Web.Controllers
{
    using ImdbLite.Data.UnitOfWork;
    using System;
    using System.Collections;
    using System.Web.Mvc;
    using System.Linq;
    using AutoMapper.QueryableExtensions;

    using DbModel = ImdbLite.Data.Models.Celebrity;
    using GridViewModel = ImdbLite.Web.ViewModels.Celebrities.CelebrityGridViewModel;
    using DetailedViewModel = ImdbLite.Web.ViewModels.Celebrities.CelebrityDetailsViewModel;
    using BiographyViewModel = ImdbLite.Web.ViewModels.Celebrities.CelebrityFullBiographyViewModel;
    using ImdbLite.Web.ViewModels.Photos;
    using ImdbLite.Web.ViewModels.Articles;

    public class CelebritiesController : BaseEntityController
    {
        private const int PageSize = 12;

        public CelebritiesController(IImdbLiteData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ReadData(int? Id)
        {
            int pageNumber = Id.GetValueOrDefault(1);
            var count = (double)GetData<GridViewModel>().Count();
            var data = GetData<GridViewModel>().OrderBy(x => x.FullName).Skip((pageNumber - 1) * PageSize).Take(PageSize);

            ViewBag.Pages = Math.Ceiling(count / PageSize);
            ViewBag.CurrentPage = pageNumber;
            ViewBag.PreviousPage = pageNumber - 1;
            ViewBag.NextPage = pageNumber + 1;

            return PartialView("_CelebrityGridPartial", data);
        }

        public ActionResult GetGallery(int id)
        {
            var model = this.Data.Celebrities
                .All()
                .Where(c => c.Id == id)
                .SelectMany(c => c.Gallery)
                .Project()
                .To<PhotoIndexViewModel>()
                .ToList();

            return PartialView("_GalleryPartial", model);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            var model = base.GetViewModel<DbModel, DetailedViewModel>(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult FullBiography(int? id)
        {
            var model = base.GetViewModel<DbModel, BiographyViewModel>(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Celebrities.GetById(id) as T;
        }

        protected override IQueryable<TViewModel> GetData<TViewModel>()
        {
            return this.Data.Celebrities.All().Project().To<TViewModel>();
        }
    }
}