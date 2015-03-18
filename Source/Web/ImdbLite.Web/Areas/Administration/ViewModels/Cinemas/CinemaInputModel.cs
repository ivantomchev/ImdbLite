namespace ImdbLite.Web.Areas.Administration.ViewModels.Cinemas
{
    using AutoMapper;
    using ImdbLite.Data.Models;
    using ImdbLite.Web.Infrastructure.Mapping;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;

    public class CinemaInputModel : IMapFrom<Cinema>, IHaveCustomMappings
    {
        public CinemaInputModel()
        {
            this.Movies = new HashSet<Movie>();
        }

        [HiddenInput]
        public int Id { get; set; }

        [UIHint("SingleLineText")]
        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string Name { get; set; }

        [UIHint("SmallMultiLineText")]
        [Required]
        [StringLength(250, MinimumLength = 2)]
        public string Address { get; set; }

        [UIHint("SingleLineText")]
        [Display(Name = "Web Address")]
        public string WebAddress { get; set; }

        [Display(Name = "City")]
        //[HiddenInput(DisplayValue = false)]
        public int CityId { get; set; }

        [UIHint("SingleLineText")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [UIHint("SingleLineText")]
        [Display(Name = "E-Mail")]
        public string EMail { get; set; }

        [UIHint("SingleLineText")]
        [Display(Name = "Google Maps URL")]
        [Required]
        public string GoogleMapsUrl { get; set; }

        [Display(Name = "Movies")]
        public int[] selectedMovies { get; set; }

        public ICollection<Movie> Movies { get; set; }

        public IEnumerable<SelectListItem> CitiesList { get; set; }

        public IEnumerable<SelectListItem> MoviesList { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Cinema, CinemaInputModel>()
                .ForMember(x => x.selectedMovies, opt => opt.MapFrom(s => s.Movies.Select(x => x.Id)));
        }
    }
}