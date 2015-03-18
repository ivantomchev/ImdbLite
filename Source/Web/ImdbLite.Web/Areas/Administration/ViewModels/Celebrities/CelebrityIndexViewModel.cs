namespace ImdbLite.Web.Areas.Administration.ViewModels.Celebrities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using ImdbLite.Common;
    using ImdbLite.Data.Models;
    using ImdbLite.Web.Areas.Administration.ViewModels.Base;
    using ImdbLite.Web.Infrastructure.Mapping;
    using AutoMapper;

    public class CelebrityIndexViewModel : NotDeletedIndexViewModel, IMapFrom<Celebrity>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Display(Name = "Full Name")]
        //[Display(ShortName = "FullName")]
        //[DisplayFormat(NullDisplayText = string.Empty)]
        public string FullName { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = GlobalConstants.DateTimeFullFormatString)]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        [DisplayFormat(NullDisplayText = "")]
        public CelebrityMainPhoto Photo { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Celebrity, CelebrityIndexViewModel>()
                .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.FirstName + " " + s.LastName));
        }
    }
}