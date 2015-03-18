namespace ImdbLite.Web.Areas.Administration.ViewModels.Photos
{
    using AutoMapper;
    using ImdbLite.Data.Models;
    using ImdbLite.Web.Infrastructure.Mapping;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using System.Web.Mvc;
    using System.Linq;

    public class PhotoUpdateModel : IMapFrom<Photo>, IHaveCustomMappings
    {
        public PhotoUpdateModel()
        {
            this.RelatedMovies = new HashSet<Movie>();
            this.RelatedCelebrities = new HashSet<Celebrity>();
        }

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        [UIHint("SingleLineText")]
        public string Title { get; set; }

        public string Url { get; set; }

        [UIHint("UploadFile")]
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
            configuration.CreateMap<Photo, PhotoUpdateModel>()
                .ForMember(x => x.selectedMovies, opt => opt.MapFrom(s => s.RelatedMovies.Select(x => x.Id)))
                .ForMember(x => x.selectedCelebrities, opt => opt.MapFrom(s => s.RelatedCelebrities.Select(x => x.Id)));
        }
    }
}