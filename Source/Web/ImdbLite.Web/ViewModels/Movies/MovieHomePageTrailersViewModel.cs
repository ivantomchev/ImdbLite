namespace ImdbLite.Web.ViewModels.Movies
{
    using System;
    using ImdbLite.Data.Models;
    using ImdbLite.Web.Infrastructure.Mapping;

    public class MovieHomePageTrailersViewModel : IMapFrom<Movie>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public MoviePoster Poster { get; set; }

        public string OfficialTrailer { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}