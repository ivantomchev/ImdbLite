namespace ImdbLite.Web.Areas.Administration.ViewModels.Movies
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using System.Web;

    using AutoMapper;

    using ImdbLite.Web.Infrastructure.Mapping;
    using ImdbLite.Data.Models;
    using ImdbLite.Web.Areas.Administration.ViewModels.Characters;
    using ImdbLite.Web.Infrastructure.Attributes;
    using ImdbLite.Services.Data.DTOs;

    public class MovieUpdateModel : IMapFrom<MovieDTO>, IHaveCustomMappings, IMovieInputModel
    {
        public MovieUpdateModel()
        {
            this.Characters = new List<CharacterInputModel>();
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

        //TODO Validate size, aspect ratio...
        [UIHint("UploadFile")]
        public HttpPostedFileBase FileToUpload { get; set; }

        public MoviePoster Poster { get; set; }

        public IList<CharacterInputModel> Characters { get; set; }

        public IEnumerable<SelectListItem> Celebrities { get; set; }

        public IEnumerable<SelectListItem> GenresList { get; set; }

        public IEnumerable<SelectListItem> CinemasList { get; set; }

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
            configuration.CreateMap<MovieUpdateModel, MovieDTO>()
                .ForMember(d => d.Genres, opt => opt.MapFrom(s => s.selectedGenres))
                .ForMember(d => d.Directors, opt => opt.MapFrom(s => s.selectedDirectors))
                .ForMember(d => d.Producers, opt => opt.MapFrom(s => s.selectedProducers))
                .ForMember(d => d.Writers, opt => opt.MapFrom(s => s.selectedWriters))
                .ForMember(d => d.Characters, opt => opt.MapFrom(s => s.selectedCharacters))
                .ForMember(d => d.Cinemas, opt => opt.MapFrom(s => s.selectedCinemas))
                .ForMember(d => d.Poster, opt => opt.MapFrom(s => s.FileToUpload != null ? Mapper.Map<MoviePoster>(s.FileToUpload) : s.Poster));

            configuration.CreateMap<MovieDTO, MovieUpdateModel>()
                .ForMember(d => d.selectedGenres, opt => opt.MapFrom(s => s.Genres))
                .ForMember(d => d.selectedDirectors, opt => opt.MapFrom(s => s.Directors))
                .ForMember(d => d.selectedProducers, opt => opt.MapFrom(s => s.Producers))
                .ForMember(d => d.selectedWriters, opt => opt.MapFrom(s => s.Writers))
                .ForMember(d => d.selectedCharacters, opt => opt.MapFrom(s => s.Characters))
                .ForMember(d => d.selectedCinemas, opt => opt.MapFrom(s => s.Cinemas));
        }

    }
}