namespace ImdbLite.Web.Areas.Administration.ViewModels.Celebrities
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    using AutoMapper;

    using ImdbLite.Data.Models;
    using ImdbLite.Web.Infrastructure.Mapping;
    using System;

    public class CelebrityUpdateModel : IMapFrom<Celebrity>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        [Display(Name = "First Name")]
        [UIHint("SingleLineText")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        [Display(Name = "Last Name")]
        [UIHint("SingleLineText")]
        public string LastName { get; set; }

        public string FullName { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 2)]
        [Display(Name = "Birth Name")]
        [UIHint("SingleLineText")]
        public string BirthName { get; set; }

        [Required]
        [MinLength(20)]
        [UIHint("MultiLineText")]
        public string Biography { get; set; }

        //TODO Create custom range attribute
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        //TODO Validate size, aspect ratio...
        [UIHint("UploadFile")]
        public HttpPostedFileBase FileToUpload { get; set; }

        public CelebrityMainPhoto Photo { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<CelebrityUpdateModel, Celebrity>()
                .ForMember(d => d.Photo, opt => opt.MapFrom(s => s.FileToUpload != null ? Mapper.Map<CelebrityMainPhoto>(s.FileToUpload) : s.Photo));

            configuration.CreateMap<Celebrity, CelebrityUpdateModel>()
                .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.FirstName + " " + s.LastName));
        }
    }
}