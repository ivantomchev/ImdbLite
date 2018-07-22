namespace ImdbLite.Services.Data.DTOs
{
    public class CharacterDTO
    {
        public int Id { get; set; }

        public int MovieId { get; set; }

        public string CelebrityName { get; set; }

        public int CelebrityId { get; set; }

        public string Name { get; set; }
    }
}
