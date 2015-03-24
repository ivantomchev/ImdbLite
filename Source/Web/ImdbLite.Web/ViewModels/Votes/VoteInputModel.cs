namespace ImdbLite.Web.ViewModels.Votes
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using ImdbLite.Data.Models;
    using ImdbLite.Web.Infrastructure.Mapping;

    public class VoteInputModel : IMapFrom<Vote>, IHaveCustomMappings
    {
        public int VoteId { get; set; }

        [Display(Name="Your Rating")]
        public double Value { get; set; }

        public string VotedById { get; set; }

        public int MovieId { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<VoteInputModel, Vote>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.VoteId));

            configuration.CreateMap<Vote, VoteInputModel>()
                .ForMember(d => d.VoteId, opt => opt.MapFrom(s => s.Id));
        }
    }
}