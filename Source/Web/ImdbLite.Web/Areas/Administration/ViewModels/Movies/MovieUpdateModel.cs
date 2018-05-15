namespace ImdbLite.Web.Areas.Administration.ViewModels.Movies
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using System.Linq;
    using System.Web;

    using AutoMapper;

    using ImdbLite.Web.Infrastructure.Mapping;
    using ImdbLite.Data.Models;
    using ImdbLite.Web.Areas.Administration.ViewModels.CastMembers;
    using ImdbLite.Web.Areas.Administration.ViewModels.Characters;
    using ImdbLite.Web.Infrastructure.Attributes;

    public class MovieUpdateModel : IMapFrom<Movie>, IHaveCustomMappings, IMovieInputModel
    {
        public MovieUpdateModel()
        {
            this.Characters = new List<CharacterInputModel>();
            this.CastMembers = new List<CastMemberInputModel>();
            this.Genres = new List<Genre>();
            this.Cinemas = new List<Cinema>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 2)]
        [UIHint("SingleLineText")]
        public string Title { get; set; }

        [UIHint("MultiLineText")]
        [Display(Name = "Story Line")]
        [MinLength(20)]
        [Required]
        public string StoryLine { get; set; }

        [Required]
        [Range(10, 400)]
        [UIHint("DurationInMinutes")]
        public int Duration { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        [DataType(DataType.DateTime)]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [Display(Name = "DVD Release Date")]
        [DataType(DataType.DateTime)]
        public DateTime DVDReleaseDate { get; set; }

        [Required]
        [UIHint("SingleLineText")]
        [Display(Name = "Official Trailer")]
        public string OfficialTrailer { get; set; }

        [UIHint("SingleLineText")]
        public int Kur { get; set; }

        [UIHint("SingleLineText")]
        public int Shano { get; set; }

        //TODO Validate size, aspect ratio...
        [UIHint("UploadFile")]
        public HttpPostedFileBase FileToUpload { get; set; }

        public MoviePoster Poster { get; set; }

        public IList<CharacterInputModel> Characters { get; set; }

        public ICollection<CastMemberInputModel> CastMembers { get; set; }

        public IEnumerable<SelectListItem> Celebrities { get; set; }

        public IEnumerable<SelectListItem> GenresList { get; set; }

        public IEnumerable<SelectListItem> CinemasList { get; set; }

        public ICollection<Cinema> Cinemas { get; set; }

        public ICollection<Genre> Genres { get; set; }

        [Display(Name = "Cinemas")]
        public int[] selectedCinemas { get; set; }

        [Display(Name = "Producers")]
        [RequiredMinimumElements(1, ErrorMessage = "At least 1 producer is required.")]
        public int[] selectedProducers { get; set; }

        [Display(Name = "Directors")]
        [RequiredMinimumElements(1, ErrorMessage = "At least 1 director is required.")]
        public int[] selectedDirectors { get; set; }

        [Display(Name = "Writers")]
        [RequiredMinimumElements(1, ErrorMessage = "At least 1 writer is required.")]
        public int[] selectedWriters { get; set; }

        [Display(Name = "Actors")]
        [RequiredMinimumElements(1, ErrorMessage = "At least 1 character is required.")]
        public int[] selectedCharacters { get; set; }

        [Display(Name = "Genres")]
        [RequiredMinimumElements(1, ErrorMessage = "At least 1 genre is required.")]
        public int[] selectedGenres { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            //Projest From ViewModel To DbModel
            configuration.CreateMap<MovieUpdateModel, Movie>()
                .ForMember(d => d.Poster, opt => opt.MapFrom(s => s.FileToUpload != null ? Mapper.Map<MoviePoster>(s.FileToUpload) : s.Poster));

            ////Project From DbModel To ViewModel
            configuration.CreateMap<Movie, MovieUpdateModel>()
                .ForMember(d => d.selectedGenres, opt => opt.MapFrom(s => s.Genres.Select(x => x.Id)))
                .ForMember(d => d.selectedDirectors, opt => opt.MapFrom(s => s.CastMembers.Where(x => x.Participation == ParticipationType.Director).Select(x => x.CelebrityId)))
                .ForMember(d => d.selectedProducers, opt => opt.MapFrom(s => s.CastMembers.Where(x => x.Participation == ParticipationType.Producer).Select(x => x.CelebrityId)))
                .ForMember(d => d.selectedWriters, opt => opt.MapFrom(s => s.CastMembers.Where(x => x.Participation == ParticipationType.Writer).Select(x => x.CelebrityId)))
                .ForMember(d => d.selectedCharacters, opt => opt.MapFrom(s => s.Characters.Select(x => x.CelebrityId)))
                .ForMember(d => d.selectedCinemas, opt => opt.MapFrom(s => s.Cinemas.Select(x => x.Id)));
        }

    }
}