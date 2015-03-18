namespace ImdbLite.Web.ViewModels.Celebrities
{
    using AutoMapper;
    using ImdbLite.Data.Models;
    using ImdbLite.Web.Infrastructure.Mapping;

    public class CelebrityArticleListItemViewModel : IMapFrom<Celebrity>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Celebrity, CelebrityArticleListItemViewModel>()
                .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.FirstName + " " + s.LastName));
        }
    }
}