namespace ImdbLite.Web.Areas.Administration.ViewModels.Genres
{
    using ImdbLite.Web.Infrastructure.Mapping;
    using ImdbLite.Web.Areas.Administration.ViewModels.Base;
    using ImdbLite.Services.Data.DTOs;

    public class GenreIndexViewModel : NotDeletedIndexViewModel, IMapFrom<GenreDTO>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}