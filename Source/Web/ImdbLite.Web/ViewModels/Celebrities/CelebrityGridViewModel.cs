namespace ImdbLite.Web.ViewModels.Celebrities
{
    using ImdbLite.Data.Models;
    using ImdbLite.Web.Infrastructure.Mapping;
    using System.Web.Mvc;
    using System.Collections;
    using AutoMapper;

    public class CelebrityGridViewModel : IMapFrom<Celebrity>,IHaveCustomMappings
    {
        [HiddenInput]
        public int Id { get; set; }

        public string FullName { get; set; }

        public CelebrityMainPhoto Photo { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Celebrity, CelebrityGridViewModel>()
                .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.FirstName + " " + s.LastName));
        }
    }
}