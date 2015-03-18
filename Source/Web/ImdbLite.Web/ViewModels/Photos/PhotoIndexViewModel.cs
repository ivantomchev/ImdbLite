namespace ImdbLite.Web.ViewModels.Photos
{
    using ImdbLite.Data.Models;
    using ImdbLite.Web.Infrastructure.Mapping;

    public class PhotoIndexViewModel : IMapFrom<Photo>
    {
        public int Id { get; set; }

        public string Url { get; set; }
    }
}