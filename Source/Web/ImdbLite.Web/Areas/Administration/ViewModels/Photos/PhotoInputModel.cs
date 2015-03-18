namespace ImdbLite.Web.Areas.Administration.ViewModels.Photos
{
    using AutoMapper;
    using ImdbLite.Common;
    using ImdbLite.Data.Models;
    using ImdbLite.Web.Infrastructure.Mapping;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Web;
    using System.Web.Mvc;

    public class PhotoInputModel : IMapFrom<Photo>, IHaveCustomMappings
    {
        public PhotoInputModel()
        {
            this.RelatedMovies = new HashSet<Movie>();
            this.RelatedCelebrities = new HashSet<Celebrity>();
        }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        [UIHint("SingleLineText")]
        public string Title { get; set; }

        [UIHint("UploadFile")]
        [Required]
        public HttpPostedFileBase FileToUpload { get; set; }

        public ICollection<Celebrity> RelatedCelebrities { get; set; }

        public ICollection<Movie> RelatedMovies { get; set; }

        public IEnumerable<SelectListItem> CelebritiesSelectList { get; set; }

        public IEnumerable<SelectListItem> MoviesSelectList { get; set; }

        [Display(Name = "Related Celebrities")]
        public int[] selectedCelebrities { get; set; }

        [Display(Name = "Related Movies")]
        public int[] selectedMovies { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<PhotoInputModel, Photo>()
                .ForMember(d => d.Url, opt => opt.MapFrom(s => Path.Combine(GlobalConstants.ImageDirectory, s.FileToUpload.GetHashCode() + s.FileToUpload.FileName.GetHashCode() + Path.GetExtension(s.FileToUpload.FileName))));
        }
    }
}