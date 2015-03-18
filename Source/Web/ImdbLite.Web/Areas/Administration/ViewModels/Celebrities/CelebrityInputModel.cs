namespace ImdbLite.Web.Areas.Administration.ViewModels.Celebrities
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    using AutoMapper;

    using ImdbLite.Data.Models;
    using ImdbLite.Web.Infrastructure.Mapping;
    using System;
    using ImdbLite.Common;

    public class CelebrityInputModel : IMapFrom<Celebrity>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        [Display(Name="First Name")]
        [UIHint("SingleLineText")]       
        public string FirstName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        [Display(Name = "Last Name")]
        [UIHint("SingleLineText")]
        public string LastName { get; set; }

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
        [Display(Name="Birth Date")]
        public DateTime BirthDate { get; set; }

        //TODO Validate size, aspect ratio...
        [UIHint("UploadFile")]
        [Required(ErrorMessage = "Please Select Image")]
        public HttpPostedFileBase FileToUpload { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<CelebrityInputModel, Celebrity>()
                .ForMember(d => d.Photo, opt => opt.MapFrom(s => s.FileToUpload));
        }
    }
}