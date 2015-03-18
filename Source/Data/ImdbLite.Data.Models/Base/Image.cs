namespace ImdbLite.Data.Models.Base
{
    using System.ComponentModel.DataAnnotations;

    public abstract class Image
    {
        [Required]
        public byte[] Content { get; set; }

        public string FileExtension { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
