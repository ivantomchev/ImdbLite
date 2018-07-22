namespace ImdbLite.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using AutoMapper;

    using ImdbLite.Common;
    using ImdbLite.Common.Extensions;
    using ImdbLite.Services.Data.DTOs;
    using ImdbLite.Services.Data.Interfaces;
    using ImdbLite.Web.Areas.Administration.ViewModels.Characters;
    using ImdbLite.Web.Areas.Administration.ViewModels.Movies;
    using ImdbLite.Web.Infrastructure.Populators;

    public class MoviesController : Controller
    {
        private const int PageSize = 10;

        private readonly IDropDownListPopulator _populator;
        private readonly IMoviesService _moviesService;

        public MoviesController(IDropDownListPopulator populator, IMoviesService moviesService)
        {     
            _populator = populator;
            _moviesService = moviesService;
        }

        public ActionResult Index() => View();

        public async Task<ActionResult> ReadData(int page = 1)
        {
            var movies = await _moviesService.GetAsync((uint)(page - 1) * PageSize, PageSize, GlobalConstants.DESC);
            var totalCount = await _moviesService.GetCountAsync();

            var viewModel = Mapper.Map<List<MovieIndexViewModel>>(movies);

            ViewBag.Pages = Math.Ceiling((double)totalCount / PageSize);
            ViewBag.CurrentPage = page;
            ViewBag.PreviousPage = page - 1;
            ViewBag.NextPage = page + 1;

            return PartialView("_ReadMoviesPartial", viewModel);
        }

        [HttpPost]
        public ActionResult AddCharacters(IList<CharacterInputModel> dynamicallyAddedCharacters)
        {
            var model = new MovieInputModel();
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

            var viewModel = Mapper.Map<MovieUpdateModel>(movie);
            viewModel.Celebrities = _populator.Celebrities.ToSelectList(x => x.Value, x => x.Key).ToList();
            viewModel.GenresList = _populator.Genres.ToSelectList(x => x.Value, x => x.Key);
            viewModel.CinemasList = _populator.Cinemas.ToSelectList(x => x.Value, x => x.Key);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(MovieUpdateModel model)
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
            var model = new MovieInputModel();
            model.Celebrities = _populator.Celebrities.ToSelectList(x => x.Value, x => x.Key);
            model.GenresList = _populator.Genres.ToSelectList(x => x.Value, x => x.Key);
            model.CinemasList = _populator.Cinemas.ToSelectList(x => x.Value, x => x.Key);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MovieInputModel model)
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
    }
}