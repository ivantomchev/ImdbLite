namespace ImdbLite.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using ImdbLite.Data.UnitOfWork;
    using ImdbLite.Web.ViewModels.Movies;
    using ImdbLite.Web.ViewModels.Photos;
    using ImdbLite.Web.ViewModels.Votes;

    using DbModel = ImdbLite.Data.Models.Movie;
    using DetailedViewModel = ImdbLite.Web.ViewModels.Movies.MovieDetailsViewModel;
    using GridViewModel = ImdbLite.Web.ViewModels.Movies.MoviesGridViewModel;

    public class MoviesController : BaseEntityController
    {
        private const int PageSize = 12;

        public MoviesController(IImdbLiteData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ReadData(int? Id)
        {
            int pageNumber = Id.GetValueOrDefault(1);

            var count = (double)GetData<GridViewModel>()
                .Count();

            var data = GetData<GridViewModel>()
                .OrderBy(x => x.Title)
                .Skip((pageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            ViewBag.Pages = Math.Ceiling(count / PageSize);
            ViewBag.CurrentPage = pageNumber;
            ViewBag.PreviousPage = pageNumber - 1;
            ViewBag.NextPage = pageNumber + 1;

            return PartialView("_MoviesGridPartial", data);
        }

        [HttpGet]
        public ActionResult GetMoviesWithRatingsListItems(int? take, string direction = "asc")
        {
            var query = this.Data.Movies
                .All()
                .Project()
                .To<MovieTopBottomListViewModel>();

            if (direction == "asc")
            {
                query = query.OrderBy(x => x.Rating);
                ViewBag.Title = "Bottom 10 Movies";
            }
            else
            {
                query = query.OrderByDescending(x => x.Rating);
                ViewBag.Title = "Top 10 Movies";
            }

            var model = query
                .Take(take.GetValueOrDefault(10))
                .ToList();

            return PartialView("_MoviesWithRatingsListItemsPartial", model);
        }

        [HttpGet]
        public ActionResult GetMoviesListItems(int? take, string direction = "asc")
        {
            var query = this.Data.Movies
                .All()
                .Project()
                .To<MovieCinemaListItemViewModel>();

            if (direction == "asc")
            {
                query = query.OrderBy(x => x.CreatedOn);
                ViewBag.Title = "First Added Movies";
            }
            else
            {
                query = query.OrderByDescending(x => x.CreatedOn);
                ViewBag.Title = "Last Added Movies";
            }

            var model = query
                .Take(take.GetValueOrDefault(10))
                .ToList();

            return PartialView("_MoviesListItemsPartial", model);
        }

        [HttpGet]
        public ActionResult GetMoviesOnTheathers(int? take, string direction = "asc")
        {
            var query = this.Data.Movies
                .All()
                .Where(m => m.Cinemas.Any())
                .Project()
                .To<MovieHomePageOnTheathersViewModel>();

            if (direction == "asc")
            {
                query = query.OrderBy(x => x.CreatedOn);
            }
            else
            {
                query = query.OrderByDescending(x => x.CreatedOn);
            }

            var model = query
                .Take(take.GetValueOrDefault(10))
                .ToList();

            return PartialView("_MoviesOnTheathersPartial", model);

        }

        [HttpGet]
        public ActionResult GetMoviesTrailers()
        {
            var model = this.Data.Movies
                .All()
                .OrderByDescending(x => x.CreatedOn)
                .Take(3)
                .Project()
                .To<MovieHomePageTrailersViewModel>()
                .ToList();

            return PartialView("_MoviesTrailersPartial", model);
        }

        [HttpGet]
        public ActionResult GetGallery(int id)
        {
            var model = this.Data.Movies.GetById(id).Gallery.AsQueryable().Project().To<PhotoIndexViewModel>().ToList();

            return PartialView("_GalleryPartial", model);
        }

        [HttpGet]
        public ActionResult GetMovieAvarageRating(int Id)
        {
            var model = new RatingViewModel();
            model.Rating = this.Data.Movies.GetById(Id).Votes.Select(x => x.Value).DefaultIfEmpty(0).Average();

            return PartialView("_MovieRatingPartial", model);
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