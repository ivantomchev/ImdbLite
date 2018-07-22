namespace ImdbLite.Services.Data.DTOs
{
    using System;
    using System.Collections.Generic;

    using ImdbLite.Data.Models;
    using ImdbLite.Services.Data.DTOs.Base;

    public class MovieDTO : BaseDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string StoryLine { get; set; }

        public int Duration { get; set; }

        public DateTime TheaterReleaseDate { get; set; }

        public DateTime? DVDReleaseDate { get; set; }

        public string OfficialTrailer { get; set; }

        public IEnumerable<int> Cinemas { get; set; }

        public IEnumerable<int> Genres { get; set; }

        public IEnumerable<int> Producers { get; set; }

        public IEnumerable<int> Directors { get; set; }

        public IEnumerable<int> Writers { get; set; }

        public IEnumerable<CharacterDTO> Characters { get; set; }

        public MoviePoster Poster { get; set; }
    }

    public class CharacterDTO
    {
        public int Id { get; set; }

        public int MovieId { get; set; }

        public string CelebrityName { get; set; }

        public int CelebrityId { get; set; }

        public string Name { get; set; }
    }
}
