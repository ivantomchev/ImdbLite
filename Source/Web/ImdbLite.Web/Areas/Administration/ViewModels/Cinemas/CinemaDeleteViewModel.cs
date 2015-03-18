namespace ImdbLite.Web.Areas.Administration.ViewModels.Cinemas
{
    using ImdbLite.Data.Models;
    using ImdbLite.Web.Infrastructure.Mapping;

    public class CinemaDeleteViewModel : IMapFrom<Cinema>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}