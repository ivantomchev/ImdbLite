namespace ImdbLite.Web.ViewModels.Movies
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;

    using ImdbLite.Common;
    using ImdbLite.Data.Models;
    using ImdbLite.Web.Infrastructure.Mapping;
    using ImdbLite.Web.ViewModels.CastMember;
    using ImdbLite.Web.ViewModels.Character;
    using ImdbLite.Web.ViewModels.Cinemas;
    using ImdbLite.Web.ViewModels.Articles;

    public class MovieDetailsViewModel : IMapFrom<Movie>, IHaveCustomMappings
    {
        [HiddenInput]
        public int Id { get; set; }

        public string Title { get; set; }

        [Display(Name = "Story Line")]
        public string StoryLine { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = GlobalConstants.DateTimeYearFormatString)]
        public DateTime Year 
        {
            get
            {
                return this.ReleaseDate;
            }
        }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = GlobalConstants.DateTimeDetailedFormatString)]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "On DVD")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = GlobalConstants.DateTimeDetailedFormatString)]
        public DateTime DVDReleaseDate { get; set; }

        [Display(Name = "Official Trailer")]
        public string OfficialTrailer { get; set; }

        public MoviePoster Poster { get; set; }

        public int Duration { get; set; }

        [UIHint("GenresList")]
        public IList<Genre> Genres { get; set; }

        [Display(Name = "Directed By")]
        [UIHint("CastMembersList")]
        public IList<CastMemberMovieViewModel> DirectedBy { get; set; }

        [Display(Name = "Produced By")]
        [UIHint("CastMembersList")]
        public IList<CastMemberMovieViewModel> ProducedBy { get; set; }

        [Display(Name = "Written By")]
        [UIHint("CastMembersList")]
        public IList<CastMemberMovieViewModel> WrittenBy { get; set; }

        [UIHint("Cast")]
        public IEnumerable<CharacterMovieViewModel> Cast { get; set; }

        [UIHint("Cinemas")]
        public IEnumerable<CinemaMovieDetailsViewModel> Cinemas { get; set; }

        [UIHint("ArticleListItems")]
        [Display(Name = "Related Articles")]
        public IEnumerable<ArticleListItemsViewModel> RelatedArticles { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Movie, MovieDetailsViewModel>()
                .ForMember(d => d.Cast, opt => opt.MapFrom(s => s.Characters))
                .ForMember(d => d.DirectedBy, opt => opt.MapFrom(s => s.CastMembers.Where(x => x.Participation == ParticipationType.Director)))
                .ForMember(d => d.ProducedBy, opt => opt.MapFrom(s => s.CastMembers.Where(x => x.Participation == ParticipationType.Producer)))
                .ForMember(d => d.WrittenBy, opt => opt.MapFrom(s => s.CastMembers.Where(x => x.Participation == ParticipationType.Writer)));
        }
    }
}