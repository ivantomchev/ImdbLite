namespace ImdbLite.Web.ViewModels.Movies
{
    using ImdbLite.Data.Models;
    using ImdbLite.Web.Infrastructure.Mapping;

    public class MovieHomePageOnTheathersViewModel : IMapFrom<Movie>
    {
        public int Id { get; set; }

        public MoviePoster Poster { get; set; }
    }
}