namespace ImdbLite.Web.Areas.Administration.ViewModels.Movies
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using System.Web.Mvc;

    using ImdbLite.Data.Models;
    using ImdbLite.Web.Areas.Administration.ViewModels.Characters;
    using System;

    public interface IMovieInputModel
    {
        int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 2)]
        [UIHint("SingleLineText")]
        string Title { get; set; }

        [UIHint("MultiLineText")]
        [Display(Name = "Story Line")]
        [MinLength(20)]
        [Required]
        string StoryLine { get; set; }

        [Required]
        [Range(10, 400)]
        [UIHint("DurationInMinutes")]
        int Duration { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        [DataType(DataType.DateTime)]
        DateTime ReleaseDate { get; set; }

        [Required]
        [Display(Name = "DVD Release Date")]
        [DataType(DataType.DateTime)]
        DateTime DVDReleaseDate { get; set; }

        [Required]
        [UIHint("SingleLineText")]
        [Display(Name = "Official Trailer")]
        string OfficialTrailer { get; set; }

        [UIHint("UploadFile")]
        HttpPostedFileBase FileToUpload { get; set; }

        MoviePoster Poster { get; set; }

        IList<CharacterInputModel> Characters { get; set; }

        IEnumerable<SelectListItem> Celebrities { get; set; }

        IEnumerable<SelectListItem> GenresList { get; set; }

        IEnumerable<SelectListItem> CinemasList { get; set; }

        [Display(Name = "Cinemas")]
        int[] selectedCinemas { get; set; }

        [Required]
        [Display(Name = "Producers")]
        int[] selectedProducers { get; set; }

        [Required]
        [Display(Name = "Directors")]
        int[] selectedDirectors { get; set; }

        [Required]
        [Display(Name = "Writers")]
        int[] selectedWriters { get; set; }

        [Required]
        [Display(Name = "Actors")]
        int[] selectedCharacters { get; set; }

        [Required]
        [Display(Name = "Genres")]
        int[] selectedGenres { get; set; }
    }
}