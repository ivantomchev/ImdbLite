namespace ImdbLite.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using ImdbLite.Common;
    using ImdbLite.Common.Extensions;
    using ImdbLite.Data.Models;
    using ImdbLite.Data.UnitOfWork;
    using ImdbLite.Web.Areas.Administration.Controllers.Base;

    using DbModel = ImdbLite.Data.Models.Photo;
    using IndexViewModel = ImdbLite.Web.Areas.Administration.ViewModels.Photos.PhotoIndexViewModel;
    using InputModel = ImdbLite.Web.Areas.Administration.ViewModels.Photos.PhotoInputModel;
    using UpdateModel = ImdbLite.Web.Areas.Administration.ViewModels.Photos.PhotoUpdateModel;
    using DeleteViewModel = ImdbLite.Web.Areas.Administration.ViewModels.Photos.PhotoDeleteViewModel;
    using ImdbLite.Web.Infrastructure.Populators;

    public class PhotosController : AdminController
    {
        private IDropDownListPopulator populator;
        private const int PageSize = 10;

        public PhotosController(IImdbLiteData data, IDropDownListPopulator populator)
            : base(data)
        {
            this.populator = populator;
        }

        public ActionResult Index()
        {
            var data = GetData<IndexViewModel>();

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

            return PartialView("_ReadPhotosPartial", data);
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

            var dbModel = base.Update<DbModel, UpdateModel>(model, model.Id, model.FileToUpload, model.Url);
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
            model.CelebritiesSelectList = this.populator.GetCelebrities().ToSelectList(x => x.Value, x => x.Key);
            model.MoviesSelectList = this.populator.GetMovies().ToSelectList(x => x.Value, x => x.Key);

            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(InputModel model)
        {
            PopulateSelectedCelebrities(model.RelatedCelebrities, model.selectedCelebrities);
            PopulateSelectedMovies(model.RelatedMovies, model.selectedMovies);

            var dbModel = base.Create<DbModel>(model, model.FileToUpload);

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

            return PartialView("_DeletePhotoPartial", model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ActualDelete(DeleteViewModel model)
        {
            base.ActualDelete<DbModel>(model.Id, model.Url);

            return base.GridOperationAjaxRefreshData();
        }

        protected override IQueryable<TViewModel> GetData<TViewModel>()
        {
            return this.Data.Gallery.All().Project().To<TViewModel>();
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Gallery.GetById(id) as T;
        }

        protected override string GetReadDataActionUrl()
        {
            return Url.Action("ReadData", "Photos");
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