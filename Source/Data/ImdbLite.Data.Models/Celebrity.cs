namespace ImdbLite.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ImdbLite.Data.Models.Base;
    using System.ComponentModel.DataAnnotations.Schema;
    using System;

    public class Celebrity : BaseEntity
    {
        private ICollection<Character> characters;
        private ICollection<CastMember> castMembers;
        private ICollection<Photo> gallery;
        private ICollection<Article> relatedArticles;

        public Celebrity()
        {
            this.characters = new HashSet<Character>();
            this.castMembers = new HashSet<CastMember>();
            this.gallery = new HashSet<Photo>();
            this.relatedArticles = new HashSet<Article>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(60)]
        public string BirthName { get; set; }

        [Required]
        public string Biography { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        //TODO Required or NOT
        public virtual CelebrityMainPhoto Photo { get; set; }

        [Required]
        public virtual ICollection<Character> Characters
        {
            get
            {
                return this.characters;
            }
            set
            {
                this.characters = value;
            }
        }

        [Required]
        public virtual ICollection<CastMember> CastMembers
        {
            get
            {
                return this.castMembers;
            }
            set
            {
                this.castMembers = value;
            }
        }

        public virtual ICollection<Photo> Gallery 
        {
            get 
            {
                return this.gallery;
            }
            set
            {
                this.gallery = value;
            }
        }

        public virtual ICollection<Article> RelatedArticles
        {
            get
            {
                return this.relatedArticles;
            }
            set
            {
                this.relatedArticles = value;
            }
        }
    }
}
