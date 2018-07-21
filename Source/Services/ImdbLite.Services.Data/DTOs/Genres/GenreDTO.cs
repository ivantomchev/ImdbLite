namespace ImdbLite.Services.Data.DTOs
{
    using ImdbLite.Services.Data.DTOs.Base;

    public class GenreDTO : BaseDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
