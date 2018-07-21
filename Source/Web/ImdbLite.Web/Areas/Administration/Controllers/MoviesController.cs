namespace ImdbLite.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using ImdbLite.Common.Extensions;
    using ImdbLite.Data.UnitOfWork;
    using ImdbLite.Services.Data.DTOs;
    using ImdbLite.Services.Data.Interfaces;
    using ImdbLite.Web.Areas.Administration.Controllers.Base;
    using ImdbLite.Web.Areas.Administration.ViewModels.Characters;
    using ImdbLite.Web.Infrastructure.Populators;

    using DeleteViewModel = ImdbLite.Web.Areas.Administration.ViewModels.Movies.MovieDeleteViewModel;
    using IndexViewModel = ImdbLite.Web.Areas.Administration.ViewModels.Movies.MovieIndexViewModel;
    using InputModel = ImdbLite.Web.Areas.Administration.ViewModels.Movies.MovieInputModel;
    using UpdateModel = ImdbLite.Web.Areas.Administration.ViewModels.Movies.MovieUpdateModel;

    public class MoviesController : AdminController
    {
        private readonly IDropDownListPopulator _populator;
        private readonly IMoviesService _moviesService;
        private const int PageSize = 10;

        public MoviesController(IImdbLiteData data, IDropDownListPopulator populator, IMoviesService moviesService)
            : base(data)
        {
            _populator = populator;
            _moviesService = moviesService;
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

            return PartialView("_ReadMoviesPartial", data);
        }

        [HttpPost]
        public ActionResult AddCharacters(IList<CharacterInputModel> dynamicallyAddedCharacters)
        {
            var model = new InputModel();
            if (dynamicallyAddedCharacters != null)
            {
                model.Characters = dynamicallyAddedCharacters;
            };

            return PartialView("_CharactersPartial", model);
        }

        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            var movie = await _moviesService.GetByIdAsync(id);
            if (movie == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<UpdateModel>(movie);
            viewModel.Celebrities = _populator.Celebrities.ToSelectList(x => x.Value, x => x.Key).ToList();
            viewModel.GenresList = _populator.Genres.ToSelectList(x => x.Value, x => x.Key);
            viewModel.CinemasList = _populator.Cinemas.ToSelectList(x => x.Value, x => x.Key);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(UpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Celebrities = _populator.Celebrities.ToSelectList(x => x.Value, x => x.Key);
                model.GenresList = _populator.Genres.ToSelectList(x => x.Value, x => x.Key);
                model.CinemasList = _populator.Cinemas.ToSelectList(x => x.Value, x => x.Key);
                return View(model);
            }

            var movie = Mapper.Map<MovieDTO>(model);

            await _moviesService.UpdateAsync(movie);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new InputModel();
            model.Celebrities = _populator.Celebrities.ToSelectList(x => x.Value, x => x.Key);
            model.GenresList = _populator.Genres.ToSelectList(x => x.Value, x => x.Key);
            model.CinemasList = _populator.Cinemas.ToSelectList(x => x.Value, x => x.Key);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(InputModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Celebrities = _populator.Celebrities.ToSelectList(x => x.Value, x => x.Key);
                model.GenresList = _populator.Genres.ToSelectList(x => x.Value, x => x.Key);
                model.CinemasList = _populator.Cinemas.ToSelectList(x => x.Value, x => x.Key);
                return View(model);
            }

            var movie = Mapper.Map<MovieDTO>(model);

            var result = await _moviesService.AddAsync(movie);
            if (result == null)
            {
                return View("Error");
            }

            return RedirectToAction("Index");
        }

        //[HttpGet]
        //public ActionResult ActualDelete(int? id)
        //{
        //    var model = base.GetViewModel<DbModel, DeleteViewModel>(id);

        //    return PartialView("_DeleteMoviePartial", model);
        //}

        //[ValidateAntiForgeryToken]
        //[HttpPost]
        //public ActionResult ActualDelete(DeleteViewModel model)
        //{
        //    base.ActualDelete<DbModel>(model.Id);

        //    return base.GridOperationAjaxRefreshData();
        //}

        protected override T GetById<T>(object id)
            => this.Data.Movies.GetById(id) as T;

        protected override IQueryable<TViewModel> GetData<TViewModel>()
            => this.Data.Movies.All().Project().To<TViewModel>();

        protected override string GetReadDataActionUrl()
            => Url.Action("ReadData", "Movies");
    }
}