namespace ImdbLite.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Collections.Generic;

    using AutoMapper.QueryableExtensions;

    using ImdbLite.Common.Extensions;
    using ImdbLite.Data.UnitOfWork;
    using ImdbLite.Web.Areas.Administration.Controllers.Base;
    using ImdbLite.Data.Models;

    using DbModel = ImdbLite.Data.Models.Cinema;
    using IndexViewModel = ImdbLite.Web.Areas.Administration.ViewModels.Cinemas.CinemaIndexViewModel;
    using InputModel = ImdbLite.Web.Areas.Administration.ViewModels.Cinemas.CinemaInputModel;
    using DeleteViewModel = ImdbLite.Web.Areas.Administration.ViewModels.Cinemas.CinemaDeleteViewModel;
    using ImdbLite.Web.Infrastructure.Caching;
    using ImdbLite.Web.Infrastructure.Populators;

    public class CinemasController : AdminController
    {
        private IDropDownListPopulator populator;
        private readonly ICacheService service;
        private const int PageSize = 10;

        public CinemasController(IImdbLiteData data,IDropDownListPopulator populator, ICacheService service)
            : base(data)
        {
            this.populator = populator;
            this.service = service;
        }

        public ActionResult Index()
        {
            var data = this.GetData<IndexViewModel>();

            return View(data);
        }

        public ActionResult ReadData(int? Id)
        {
            int pageNumber = Id.GetValueOrDefault(1);
            var count = (double)GetData<IndexViewModel>().Count();
            var data = GetData<IndexViewModel>().OrderByDescending(x => x.CreatedOn).Skip((pageNumber - 1) * PageSize).Take(PageSize);

            ViewBag.Pages = Math.Ceiling(count / PageSize);
            ViewBag.CurrentPage = pageNumber;
            ViewBag.PreviousPage = pageNumber - 1;
            ViewBag.NextPage = pageNumber + 1;

            return PartialView("_ReadCinemasPartial", data);
        }

        [HttpGet]
        public ActionResult Update(int? id)
        {
            var model = base.GetViewModel<DbModel, InputModel>(id);

            if (model == null)
            {
                return HttpNotFound();
            }
            model.CitiesList = this.populator.GetCities();
            model.MoviesList = this.populator.GetMovies();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(InputModel model)
        {
            PopulateSelectedMovies(model.Movies, model.selectedMovies);

            var dbModel = base.Update<DbModel, InputModel>(model, model.Id);
            if (dbModel != null)
            {
                this.ClearCinemasCache();
                return RedirectToAction("Index");
            }
            model.CitiesList = this.populator.GetCities();
            model.MoviesList = this.populator.GetMovies();

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new InputModel();
            model.CitiesList = this.populator.GetCities();
            model.MoviesList = this.populator.GetMovies();
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(InputModel model)
        {
            PopulateSelectedMovies(model.Movies, model.selectedMovies);

            var dbModel = base.Create<DbModel>(model);

            if (dbModel != null)
            {
                this.ClearCinemasCache();
                return RedirectToAction("Index");
            }
            model.CitiesList = this.populator.GetCities();
            model.MoviesList = this.populator.GetMovies();
            return View(model);
        }

        [HttpGet]
        public ActionResult ActualDelete(int? id)
        {
            var model = base.GetViewModel<DbModel, DeleteViewModel>(id);

            return PartialView("_DeleteCinemasPartial", model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ActualDelete(DeleteViewModel model)
        {
            base.ActualDelete<DbModel>(model.Id);
            this.ClearCinemasCache();

            return base.GridOperationAjaxRefreshData();
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Cinemas.GetById(id) as T;
        }

        protected override IQueryable<TViewModel> GetData<TViewModel>()
        {
            return this.Data.Cinemas.All().Project().To<TViewModel>();
        }

        protected override string GetReadDataActionUrl()
        {
            return Url.Action("ReadData", "Cinemas");
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

        private void ClearCinemasCache()
        {
            this.service.Clear("Cinemas");
        }
    }
}