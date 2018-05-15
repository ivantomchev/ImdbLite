namespace ImdbLite.Web.Controllers
{
    using ImdbLite.Data.UnitOfWork;
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;

    using DbModel = ImdbLite.Data.Models.Article;
    using IndexViewModel = ImdbLite.Web.ViewModels.Articles.ArticleIndexViewModel;
    using DetailedViewModel = ImdbLite.Web.ViewModels.Articles.ArticleDetailsViewModel;
    using ImdbLite.Web.ViewModels.Articles;
    using System.Threading;

    public class ArticlesController : BaseEntityController
    {
        private const int PageSize = 6;

        public ArticlesController(IImdbLiteData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            var data = GetData<IndexViewModel>();

            return View(data);
        }

        [HttpGet]
        public ActionResult ReadData(int? Id)
        {
            int pageNumber = Id.GetValueOrDefault(1);
            var count = (double)GetData<IndexViewModel>().Count();
            var data = GetData<IndexViewModel>().OrderBy(x => x.CreatedOn).Skip((pageNumber - 1) * PageSize).Take(PageSize);

            ViewBag.Pages = Math.Ceiling(count / PageSize);
            ViewBag.CurrentPage = pageNumber;
            ViewBag.PreviousPage = pageNumber - 1;
            ViewBag.NextPage = pageNumber + 1;

            return PartialView("_ArticlesGridPartial", data);
        }

        public ActionResult GetArticlesListItems(int? take)
        {
            var model = this.Data.Articles
                .All()
                .Project()
                .To<ArticleListItemsViewModel>()
                .Take(take.GetValueOrDefault(10))
                .ToList();

            return PartialView("_ArticlesListItemsPartial", model);
        }

        public ActionResult GetArticlesCarouselListItems(int? take)
        {
            var model = this.Data.Articles
                .All()
                .Project()
                .To<ArticleHomePageCarouselViewModel>()
                .OrderByDescending(x => x.CreatedOn)
                .Take(take.GetValueOrDefault(7))
                .ToList();

            return PartialView("_ArticlesCarouselListItemsPartial", model);
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
            return this.Data.Articles.GetById(id) as T;
        }

        protected override IQueryable<TViewModel> GetData<TViewModel>()
        {
            return this.Data.Articles.All().Project().To<TViewModel>();
        }
    }
}