namespace ImdbLite.Web.Areas.Administration.Controllers
{
    using ImdbLite.Data.UnitOfWork;
    using ImdbLite.Web.Areas.Administration.Controllers.Base;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using System;
    using ImdbLite.Common.Extensions;

    using DbModel = ImdbLite.Data.Models.Article;
    using IndexViewModel = ImdbLite.Web.Areas.Administration.ViewModels.Articles.ArticleIndexViewModel;
    using InputModel = ImdbLite.Web.Areas.Administration.ViewModels.Articles.ArticleInputModel;
    using UpdateModel = ImdbLite.Web.Areas.Administration.ViewModels.Articles.ArticleUpdateModel;
    using DeleteViewModel = ImdbLite.Web.Areas.Administration.ViewModels.Articles.ArticleDeleteViewModel;
    using System.Collections.Generic;
    using ImdbLite.Data.Models;
    using ImdbLite.Web.Infrastructure.Populators;

    public class ArticlesController : AdminController
    {
        private IDropDownListPopulator populator;
        private const int PageSize = 10;

        public ArticlesController(IImdbLiteData data, IDropDownListPopulator populator)
            : base(data)
        {
            this.populator = populator;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReadData(int? Id)
        {
            int pageNumber = Id.GetValueOrDefault(1);
            var count = (double)GetData<IndexViewModel>().Count();
            var data = GetData<IndexViewModel>().OrderBy(x => x.ModifiedOn).ThenBy(x => x.CreatedOn).Skip((pageNumber - 1) * PageSize).Take(PageSize);

            ViewBag.Pages = Math.Ceiling(count / PageSize);
            ViewBag.CurrentPage = pageNumber;
            ViewBag.PreviousPage = pageNumber - 1;
            ViewBag.NextPage = pageNumber + 1;

            return PartialView("_ReadArticlesPartial", data);
        }

        [HttpGet]
        public ActionResult Update(int? id)
        {
            var model = base.GetViewModel<DbModel, UpdateModel>(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            model.CelebritiesSelectList = this.populator.GetCelebrities().ToSelectList(x => x.Value, x => x.Key);
            model.MoviesSelectList = this.populator.GetMovies().ToSelectList(x => x.Value, x => x.Key);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UpdateModel model)
        {
            PopulateSelectedCelebrities(model.RelatedCelebrities, model.selectedCelebrities);
            PopulateSelectedMovies(model.RelatedMovies, model.selectedMovies);

            var dbModel = base.Update<DbModel, UpdateModel>(model, model.Id);
            if (dbModel != null)
            {
                return RedirectToAction("Index");
            }

            model.CelebritiesSelectList = this.populator.GetCelebrities().ToSelectList(x => x.Value, x => x.Key);
            model.MoviesSelectList = this.populator.GetMovies().ToSelectList(x => x.Value, x => x.Key);
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new InputModel();
            model.CelebritiesSelectList = this.populator.GetCelebrities().ToSelectList(x => x.Value, x => x.Key); ;
            model.MoviesSelectList = this.populator.GetMovies().ToSelectList(x => x.Value, x => x.Key);

            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(InputModel model)
        {
            PopulateSelectedCelebrities(model.RelatedCelebrities, model.selectedCelebrities);
            PopulateSelectedMovies(model.RelatedMovies, model.selectedMovies);

            var dbModel = base.Create<DbModel>(model);

            if (dbModel != null)
            {
                return RedirectToAction("Index");
            }

            model.CelebritiesSelectList = this.populator.GetCelebrities().ToSelectList(x => x.Value, x => x.Key);
            model.MoviesSelectList = this.populator.GetMovies().ToSelectList(x => x.Value, x => x.Key);
            return View(model);
        }

        [HttpGet]
        public ActionResult ActualDelete(int? id)
        {
            var model = base.GetViewModel<DbModel, DeleteViewModel>(id);

            return PartialView("_DeleteArticlePartial", model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ActualDelete(DeleteViewModel model)
        {
            base.ActualDelete<DbModel>(model.Id);

            return base.GridOperationAjaxRefreshData();
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Articles.GetById(id) as T;
        }

        protected override IQueryable<TViewModel> GetData<TViewModel>()
        {
            return this.Data.Articles.All().Project().To<TViewModel>();
        }

        protected override string GetReadDataActionUrl()
        {
            return Url.Action("ReadData", "Articles");
        }

        private void PopulateSelectedMovies(ICollection<Movie> movies, int[] selectedMovies)
        {
            if (selectedMovies != null)
            {
                foreach (var movieId in selectedMovies)
                {
                    var currentMovie = this.Data.Movies.GetById(movieId);

                    movies.Add(currentMovie);
                }
            }
        }

        private void PopulateSelectedCelebrities(ICollection<Celebrity> celebrites, int[] selectedCelebrities)
        {
            if (selectedCelebrities != null)
            {
                foreach (var celebrityId in selectedCelebrities)
                {
                    var currentCelebrity = this.Data.Celebrities.GetById(celebrityId);

                    celebrites.Add(currentCelebrity);
                }
            }
        }
    }
}