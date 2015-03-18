namespace ImdbLite.Web.Areas.Administration.ViewModels.Movies
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using ImdbLite.Web.Infrastructure.Mapping;
    using ImdbLite.Data.Models;
    using ImdbLite.Common;
    using ImdbLite.Web.Areas.Administration.ViewModels.Base;

    public class MovieIndexViewModel : NotDeletedIndexViewModel, IMapFrom<Movie>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = GlobalConstants.DateTimeYearFormatString)]
        [Display(Name = "Year")]
        public DateTime ReleaseDate { get; set; }

        public MoviePoster Poster { get; set; }
    }
}