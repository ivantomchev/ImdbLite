namespace ImdbLite.Web.Areas.Administration.ViewModels.Articles
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;

    using ImdbLite.Data.Models;
    using ImdbLite.Web.Infrastructure.Mapping;

    public class ArticleInputModel : IMapFrom<Article>, IHaveCustomMappings
    {
        public ArticleInputModel()
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
        [Required(ErrorMessage = "Please Select Image")]
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
            configuration.CreateMap<ArticleInputModel, Article>()
                .ForMember(d => d.Photo, opt => opt.MapFrom(s => s.FileToUpload));
        }
    }
}