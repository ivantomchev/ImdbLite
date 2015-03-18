namespace ImdbLite.Web.ViewModels.Celebrities
{
    using AutoMapper;
    using ImdbLite.Common;
    using ImdbLite.Data.Models;
    using ImdbLite.Web.Infrastructure.Mapping;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CelebrityFullBiographyViewModel : IMapFrom<Celebrity>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string FullName { get; set; }

        [Display(Name = "Birth Name")]
        public string BirthName { get; set; }

        public string Biography { get; set; }

        [Display(Name = "Birth Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = GlobalConstants.DateTimeDetailedFormatString)]
        public DateTime BirthDate { get; set; }

        public int Age
        {
            get
            {
                return DateTime.Now.Year - this.BirthDate.Year;
            }
        }

        public CelebrityMainPhoto Photo { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Celebrity, CelebrityFullBiographyViewModel>()
                .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.FirstName + " " + s.LastName));
        }
    }
}