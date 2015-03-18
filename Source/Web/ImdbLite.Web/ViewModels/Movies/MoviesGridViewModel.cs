namespace ImdbLite.Web.ViewModels.Movies
{
    using ImdbLite.Data.Models;
    using ImdbLite.Web.Infrastructure.Mapping;
    using System.Web.Mvc;

    public class MoviesGridViewModel : IMapFrom<Movie>
    {
        [HiddenInput]
        public int Id { get; set; }

        public string Title { get; set; }

        public MoviePoster Poster { get; set; }
    }
}