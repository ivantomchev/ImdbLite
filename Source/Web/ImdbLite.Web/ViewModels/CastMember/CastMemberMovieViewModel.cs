namespace ImdbLite.Web.ViewModels.CastMember
{
    using AutoMapper;
    using ImdbLite.Web.Infrastructure.Mapping;
    using ImdbLite.Data.Models;
    using System.Web.Mvc;

    public class CastMemberMovieViewModel : IMapFrom<CastMember>, IHaveCustomMappings
    {
        public string CelebrityFullName { get; set; }

        [HiddenInput]
        public string CelebrityId { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<CastMember, CastMemberMovieViewModel>()
                .ForMember(d => d.CelebrityFullName, opt => opt.MapFrom(s => s.Celebrity.FirstName + " " + s.Celebrity.LastName));
        }
    }
}