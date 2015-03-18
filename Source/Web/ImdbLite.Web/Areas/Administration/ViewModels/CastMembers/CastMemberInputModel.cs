namespace ImdbLite.Web.Areas.Administration.ViewModels.CastMembers
{
    using System.Web.Mvc;

    using ImdbLite.Web.Infrastructure.Mapping;
    using ImdbLite.Data.Models;

    public class CastMemberInputModel : IMapFrom<CastMember>
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int CelebrityId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public ParticipationType Participation { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int MovieId { get; set; }
    }
}