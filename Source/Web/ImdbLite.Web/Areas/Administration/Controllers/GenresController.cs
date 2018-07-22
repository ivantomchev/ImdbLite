namespace ImdbLite.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using AutoMapper;

    using ImdbLite.Common;
    using ImdbLite.Services.Data;
    using ImdbLite.Services.Data.DTOs;
    using ImdbLite.Web.Areas.Administration.ViewModels.Genres;

    public class GenresController : Controller
    {
        private const int PageSize = 10;
        private readonly IGenresService _genresService;

        public GenresController(IGenresService genresService)
        {
            _genresService = genresService;
        }

        public ActionResult Index() => View();

        public async Task<ActionResult> ReadData(int page = 1)
        {
            var genres = await _genresService.GetAsync((uint)(page - 1) * PageSize, PageSize, GlobalConstants.DESC);
            var totalCount = await _genresService.GetCountAsync();

            var viewModel = Mapper.Map<List<GenreIndexViewModel>>(genres);

            ViewBag.Pages = Math.Ceiling((double)totalCount / PageSize);
            ViewBag.CurrentPage = page;
            ViewBag.PreviousPage = page - 1;
            ViewBag.NextPage = page + 1;

            return PartialView("_ReadGenresPartial", viewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            var genre = await _genresService.GetByIdAsync(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            var viewModel = Mapper.Map<GenreUpdateModel>(genre);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(GenreUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var genre = await _genresService.GetByIdAsync(model.Id);
            if (genre == null)
            {
                return HttpNotFound();
            }

            Mapper.Map(model, genre);

            await _genresService.UpdateAsync(genre);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create() => View();

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Create(GenreInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var genre = Mapper.Map<GenreDTO>(model);

            var result = await _genresService.AddAsync(genre);
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

        //    return PartialView("_DeleteGenrePartial", model);
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