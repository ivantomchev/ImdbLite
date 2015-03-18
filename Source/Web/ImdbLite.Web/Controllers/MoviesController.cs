namespace ImdbLite.Web.Controllers
{
    using ImdbLite.Data.UnitOfWork;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;

    using DbModel = ImdbLite.Data.Models.Movie;
    using GridViewModel = ImdbLite.Web.ViewModels.Movies.MoviesGridViewModel;
    using DetailedViewModel = ImdbLite.Web.ViewModels.Movies.MovieDetailsViewModel;
    using ImdbLite.Web.ViewModels.Photos;
    using System;
    using ImdbLite.Web.ViewModels.Articles;

    public class MoviesController : BaseEntityController
    {
        private const int PageSize = 12;

        public MoviesController(IImdbLiteData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            var model = this.Data.Movies.All().SelectMany(x => x.RelatedArticles).Distinct().Project().To<ArticleListItemsViewModel>().Take(10);

            return View(model);
        }

        public ActionResult ReadData(int? Id)
        {
            int pageNumber = Id.GetValueOrDefault(1);
            var count = (double)GetData<GridViewModel>().Count();
            var data = GetData<GridViewModel>().OrderBy(x => x.Title).Skip((pageNumber - 1) * PageSize).Take(PageSize);

            ViewBag.Pages = Math.Ceiling(count / PageSize);
            ViewBag.CurrentPage = pageNumber;
            ViewBag.PreviousPage = pageNumber - 1;
            ViewBag.NextPage = pageNumber + 1;

            return PartialView("_MoviesGridPartial", data);
        }

        public ActionResult GetGallery(int id)
        {
            var model = this.Data.Movies.GetById(id).Gallery.AsQueryable().Project().To<PhotoIndexViewModel>().ToList();

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

        protected override T GetById<T>(object id)
        {
            return this.Data.Movies.GetById(id) as T;
        }

        protected override IQueryable<TViewModel> GetData<TViewModel>()
        {
            return this.Data.Movies.All().Project().To<TViewModel>();
        }
    }
}