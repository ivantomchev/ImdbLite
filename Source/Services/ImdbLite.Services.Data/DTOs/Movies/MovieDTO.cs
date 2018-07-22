namespace ImdbLite.Services.Data.DTOs
{
    using System;
    using System.Collections.Generic;

    public class MovieDTO : MovieListItemDTO
    {
        public string StoryLine { get; set; }

        public int Duration { get; set; }

        public DateTime? DVDReleaseDate { get; set; }

        public string OfficialTrailer { get; set; }

        public FileDTO Poster { get; set; }

        public IEnumerable<int> Cinemas { get; set; }

        public IEnumerable<int> Genres { get; set; }

        public IEnumerable<int> Producers { get; set; }

        public IEnumerable<int> Directors { get; set; }

        public IEnumerable<int> Writers { get; set; }

        public IEnumerable<CharacterDTO> Characters { get; set; }
    }
}
