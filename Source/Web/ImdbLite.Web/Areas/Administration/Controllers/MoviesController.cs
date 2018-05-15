namespace ImdbLite.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using ImdbLite.Data.UnitOfWork;
    using ImdbLite.Data.Models;
    using ImdbLite.Web.Areas.Administration.Controllers.Base;
    using ImdbLite.Web.Areas.Administration.ViewModels.Characters;
    using ImdbLite.Web.Areas.Administration.ViewModels.CastMembers;
    using ImdbLite.Common.Extensions;

    using DbModel = ImdbLite.Data.Models.Movie;
    using IndexViewModel = ImdbLite.Web.Areas.Administration.ViewModels.Movies.MovieIndexViewModel;
    using InputModel = ImdbLite.Web.Areas.Administration.ViewModels.Movies.MovieInputModel;
    using DeleteViewModel = ImdbLite.Web.Areas.Administration.ViewModels.Movies.MovieDeleteViewModel;
    using UpdateModel = ImdbLite.Web.Areas.Administration.ViewModels.Movies.MovieUpdateModel;
    using ImdbLite.Web.Infrastructure.Caching;
    using ImdbLite.Web.Infrastructure.Populators;

    public class MoviesController : AdminController
    {
        private IDropDownListPopulator populator;
        private readonly ICacheService service;
        private const int PageSize = 10;

        public MoviesController(IImdbLiteData data, IDropDownListPopulator populator, ICacheService service)
            : base(data)
        {
            this.populator = populator;
            this.service = service;
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
        public ActionResult Update(int? id)
        {
            var model = base.GetViewModel<DbModel, UpdateModel>(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            model.Celebrities = this.populator.GetCelebrities().ToSelectList(x => x.Value, x => x.Key);
            model.GenresList = this.populator.GetGenres().ToSelectList(x => x.Value, x => x.Key);
            model.CinemasList = this.populator.GetCinemas().ToSelectList(x => x.Value, x => x.Key);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UpdateModel model)
        {
            //TODO More appropriate Update Action

            //var celebritiesIds = model.selectedDirectors
            //    .Concat(model.selectedProducers)
            //    .Concat(model.selectedWriters)
            //    .Distinct();

            //var selectedCelebrities = this.Data.Celebrities
            //    .All()
            //    .Where(c => celebritiesIds.Contains(c.Id))
            //    .ToList();

            //var directors = selectedCelebrities
            //    .Where(c => model.selectedDirectors.Contains(c.Id))
            //    .Select(c => new CastMemberInputModel
            //    {
            //        CelebrityId = c.Id,
            //        Participation = ParticipationType.Director
            //    })
            //    .ToList();

            //var producers = selectedCelebrities
            //    .Where(c => model.selectedProducers.Contains(c.Id))
            //    .Select(c => new CastMemberInputModel
            //    {
            //        CelebrityId = c.Id,
            //        Participation = ParticipationType.Producer
            //    })
            //    .ToList();

            //var writers = selectedCelebrities
            //    .Where(c => model.selectedWriters.Contains(c.Id))
            //    .Select(c => new CastMemberInputModel
            //    {
            //        CelebrityId = c.Id,
            //        Participation = ParticipationType.Writer
            //    })
            //    .ToList();

            //model.CastMembers = directors
            //    .Concat(producers)
            //    .Concat(writers)
            //    .ToList();

            //model.Genres = this.Data.Genres
            //    .All()
            //    .Where(g => model.selectedGenres.Contains(g.Id))
            //    .ToList();

            //model.Cinemas = this.Data.Cinemas
            //    .All()
            //    .Where(c => model.selectedCinemas.Contains(c.Id))
            //    .ToList();

            //this.Data.Characters
            //    .All()
            //    .Where(x => x.MovieId == model.Id && !model.selectedCharacters.Contains(x.Id))
            //    .ForEach(this.Data.Characters.ActualDelete);

            //this.Data.CastMembers
            //    .All()
            //    .Where(x => x.MovieId == model.Id && !celebritiesIds.Contains(x.Id))
            //    .ForEach(this.Data.CastMembers.ActualDelete);

            //var dbModel = base.Update<DbModel, UpdateModel>(model, model.Id);
            //if (dbModel != null)
            //{
            //    this.ClearMoviesCache();
            //    return RedirectToAction("Index");
            //}

            model.Celebrities = this.populator.GetCelebrities().ToSelectList(x => x.Value, x => x.Key);
            model.GenresList = this.populator.GetGenres().ToSelectList(x => x.Value, x => x.Key);
            model.CinemasList = this.populator.GetCinemas().ToSelectList(x => x.Value, x => x.Key);
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new InputModel();
            model.Celebrities = this.populator.GetCelebrities().ToSelectList(x => x.Value, x => x.Key);
            model.GenresList = this.populator.GetGenres().ToSelectList(x => x.Value, x => x.Key);
            model.CinemasList = this.populator.GetCinemas().ToSelectList(x => x.Value, x => x.Key);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InputModel model)
        {
            PopulateSelectedGenres(model.Genres, model.selectedGenres);
            PopulateSelectedCastMembers(model.CastMembers, model.selectedDirectors, ParticipationType.Director);
            PopulateSelectedCastMembers(model.CastMembers, model.selectedProducers, ParticipationType.Producer);
            PopulateSelectedCastMembers(model.CastMembers, model.selectedWriters, ParticipationType.Writer);
            PopulateSelectedCinemas(model.Cinemas, model.selectedCinemas);

            var dbModel = base.Create<DbModel>(model);
            if (dbModel != null)
            {
                this.ClearMoviesCache();
                return RedirectToAction("Index");
            }

            model.Celebrities = this.populator.GetCelebrities().ToSelectList(x => x.Value, x => x.Key);
            model.GenresList = this.populator.GetGenres().ToSelectList(x => x.Value, x => x.Key);
            model.CinemasList = this.populator.GetCinemas().ToSelectList(x => x.Value, x => x.Key);
            return View(model);
        }

        [HttpGet]
        public ActionResult ActualDelete(int? id)
        {
            var model = base.GetViewModel<DbModel, DeleteViewModel>(id);

            return PartialView("_DeleteMoviePartial", model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ActualDelete(DeleteViewModel model)
        {
            base.ActualDelete<DbModel>(model.Id);
            this.ClearMoviesCache();

            return base.GridOperationAjaxRefreshData();
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Movies.GetById(id) as T;
        }

        protected override IQueryable<TViewModel> GetData<TViewModel>()
        {
            return this.Data.Movies.All().Project().To<TViewModel>();
        }

        protected override string GetReadDataActionUrl()
        {
            return Url.Action("ReadData", "Movies");
        }

        private void PopulateSelectedCastMembers(ICollection<CastMemberInputModel> castMembers, int[] selectedCelebrities, ParticipationType participation)
        {
            if (selectedCelebrities != null)
            {
                foreach (var celebrityId in selectedCelebrities)
                {
                    var castMember = new CastMemberInputModel();
                    castMember.CelebrityId = celebrityId;
                    castMember.Participation = participation;

                    castMembers.Add(castMember);
                }
            }
        }

        private void PopulateSelectedGenres(ICollection<Genre> genres, int[] selectedGenres)
        {
            if (selectedGenres != null)
            {
                foreach (var genreId in selectedGenres)
                {
                    var currentGenre = this.Data.Genres.GetById(genreId);

                    genres.Add(currentGenre);
                }
            }
        }

        private void PopulateSelectedCinemas(ICollection<Cinema> cinemas, int[] selectedCinemas)
        {
            if (selectedCinemas != null)
            {
                foreach (var cinemaId in selectedCinemas)
                {
                    var currentCinema = this.Data.Cinemas.GetById(cinemaId);

                    cinemas.Add(currentCinema);
                }
            }
        }

        private void ClearMovieCollections(int movieId)
        {
            var characters = this.Data.Characters.All().Where(x => x.MovieId == movieId);
            var castmembers = this.Data.CastMembers.All().Where(x => x.MovieId == movieId);

            characters.ForEach(this.Data.Characters.ActualDelete);
            castmembers.ForEach(this.Data.CastMembers.ActualDelete);
        }

        public void ClearMoviesCache()
        {
            this.service.Clear("Movies");
        }
    }
}