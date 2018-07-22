namespace ImdbLite.Services.Data.DTOs
{
    using System;

    using ImdbLite.Services.Data.DTOs.Base;

    public class MovieListItemDTO : BaseDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}
