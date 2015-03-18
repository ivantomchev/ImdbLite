namespace ImdbLite.Web.Areas.Administration.ViewModels.Genres
{
    using ImdbLite.Web.Infrastructure.Mapping;
    using ImdbLite.Data.Models;
    using ImdbLite.Web.Areas.Administration.ViewModels.Base;

    public class GenreIndexViewModel : NotDeletedIndexViewModel, IMapFrom<Genre>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}