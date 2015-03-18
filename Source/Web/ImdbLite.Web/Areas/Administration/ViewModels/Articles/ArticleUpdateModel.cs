namespace ImdbLite.Web.Areas.Administration.ViewModels.Articles
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;

    using ImdbLite.Data.Models;
    using ImdbLite.Web.Infrastructure.Mapping;


    public class ArticleUpdateModel : IMapFrom<Article>, IHaveCustomMappings
    {
        public ArticleUpdateModel()
        {
            this.RelatedMovies = new HashSet<Movie>();
            this.RelatedCelebrities = new HashSet<Celebrity>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        [UIHint("SingleLineText")]
        public string Title { get; set; }

        [Required]
        [UIHint("MultiLineText")]
        public string Content { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Written By")]
        [UIHint("SingleLineText")]
        public string WrittenBy { get; set; }

        //TODO Validate size, aspect ratio...
        [UIHint("UploadFile")]
        public HttpPostedFileBase FileToUpload { get; set; }

        public ArticlePhoto Photo { get; set; }

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
            configuration.CreateMap<Article, ArticleUpdateModel>()
                .ForMember(d => d.selectedMovies, opt => opt.MapFrom(s => s.RelatedMovies.Select(x => x.Id)))
                .ForMember(d => d.selectedCelebrities, opt => opt.MapFrom(s => s.RelatedCelebrities.Select(x => x.Id)));

            configuration.CreateMap<ArticleUpdateModel, Article>()
                .ForMember(d => d.Photo, opt => opt.MapFrom(s => s.FileToUpload != null ? Mapper.Map<ArticlePhoto>(s.FileToUpload) : s.Photo));
        }
    }
}