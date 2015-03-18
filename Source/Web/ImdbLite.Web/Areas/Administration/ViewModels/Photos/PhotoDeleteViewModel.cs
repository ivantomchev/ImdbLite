namespace ImdbLite.Web.Areas.Administration.ViewModels.Photos
{
    using ImdbLite.Data.Models;
    using ImdbLite.Web.Infrastructure.Mapping;

    public class PhotoDeleteViewModel : IMapFrom<Photo>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }
    }
}