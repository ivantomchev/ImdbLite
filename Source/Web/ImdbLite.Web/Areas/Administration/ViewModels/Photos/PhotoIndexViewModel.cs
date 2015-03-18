namespace ImdbLite.Web.Areas.Administration.ViewModels.Photos
{
    using ImdbLite.Data.Models;
    using ImdbLite.Web.Areas.Administration.ViewModels.Base;
    using ImdbLite.Web.Infrastructure.Mapping;

    public class PhotoIndexViewModel : NotDeletedIndexViewModel,IMapFrom<Photo>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }
    }
}