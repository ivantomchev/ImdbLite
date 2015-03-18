namespace ImdbLite.Web.Areas.Administration.ViewModels.Movies
{
    using ImdbLite.Web.Infrastructure.Mapping;
    using ImdbLite.Data.Models;

    public class MovieDeleteViewModel : IMapFrom<Movie>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}