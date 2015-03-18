namespace ImdbLite.Web.ViewModels.Celebrities
{
    using System.Collections.Generic;
    using System.Linq;

    using ImdbLite.Data.Models;
    using ImdbLite.Web.Infrastructure.Mapping;
    using ImdbLite.Web.ViewModels.CastMember;
    using ImdbLite.Web.ViewModels.Character;
    using ImdbLite.Web.ViewModels.Photos;
    using System;
    using System.ComponentModel.DataAnnotations;
    using ImdbLite.Common;
    using ImdbLite.Web.ViewModels.Movies;
    using AutoMapper;
    using ImdbLite.Web.ViewModels.Articles;

    public class CelebrityDetailsViewModel : IMapFrom<Celebrity>, IHaveCustomMappings
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

        [UIHint("Characters")]
        public IEnumerable<CharacterCelebrityViewModel> Actor { get; set; }

        [UIHint("Producers")]
        public IEnumerable<CastMemberCelebrityViewModel> Producer { get; set; }

        [UIHint("Directors")]
        public IEnumerable<CastMemberCelebrityViewModel> Director { get; set; }

        [UIHint("Writers")]
        public IEnumerable<CastMemberCelebrityViewModel> Writer { get; set; }

        public IEnumerable<PhotoIndexViewModel> RecentPhotos { get; set; }

        [Display(Name = "Also Known From")]
        public IEnumerable<MoviesGridViewModel> RelatedMovies { get; set; }

        [UIHint("ArticleListItems")]
        [Display(Name = "Related Articles")]
        public IEnumerable<ArticleListItemsViewModel> RelatedArticles { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            //configuration.CreateMap<Celebrity, CelebrityDetailsViewModel>()
            //    .ForMember(d => d.RelatedMovies, opt => opt.MapFrom(s => s.Characters.Join(s.CastMembers, x => x.CelebrityId, y => y.CelebrityId, (character, castmembers) => new MoviesGridViewModel { Poster = castmembers.Movie.Poster, Title = castmembers.Movie.Title, Id = castmembers.MovieId }).Take(6)))
            //    .ForMember(d => d.Biography, opt => opt.MapFrom(s => s.Biography.Substring(0, 300)))
            //    .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.FirstName + " " + s.LastName))
            //    .ForMember(d => d.Actor, opt => opt.MapFrom(s => s.Characters))
            //    .ForMember(d => d.Producer, opt => opt.MapFrom(s => s.CastMembers.Where(x => x.Participation == ParticipationType.Producer)))
            //    .ForMember(d => d.Director, opt => opt.MapFrom(s => s.CastMembers.Where(x => x.Participation == ParticipationType.Director)))
            //    .ForMember(d => d.Writer, opt => opt.MapFrom(s => s.CastMembers.Where(x => x.Participation == ParticipationType.Writer)))
            //    .ForMember(d => d.RecentPhotos, opt => opt.MapFrom(s => s.Gallery));

            configuration.CreateMap<Celebrity, CelebrityDetailsViewModel>()
                //return value.Length <= maxChars ? value : value.Substring(0, maxChars) + " ..";
                //.ForMember(d => d.Biography, opt => opt.MapFrom(s => s.Biography.Substring(0, 300)))
                .ForMember(d => d.Biography, opt => opt.MapFrom(s => s.Biography.Length <= 300 ? s.Biography : s.Biography.Substring(0,300) + ". . ."))
                .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.FirstName + " " + s.LastName))
                .ForMember(d => d.Actor, opt => opt.MapFrom(s => s.Characters))
                .ForMember(d => d.Producer, opt => opt.MapFrom(s => s.CastMembers.Where(x => x.Participation == ParticipationType.Producer)))
                .ForMember(d => d.Director, opt => opt.MapFrom(s => s.CastMembers.Where(x => x.Participation == ParticipationType.Director)))
                .ForMember(d => d.Writer, opt => opt.MapFrom(s => s.CastMembers.Where(x => x.Participation == ParticipationType.Writer)))
                .ForMember(d => d.RecentPhotos, opt => opt.MapFrom(s => s.Gallery))
                .ForMember(d => d.RelatedMovies, opt => opt.MapFrom(s => s.CastMembers.Select(x => x.Movie).Union(s.Characters.Select(y => y.Movie)).Take(6)));
        }
    }
}