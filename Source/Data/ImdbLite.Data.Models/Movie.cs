namespace ImdbLite.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ImdbLite.Data.Models.Base;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Movie : BaseEntity
    {
        private ICollection<Character> characters;
        private ICollection<CastMember> castMembers;
        private ICollection<Genre> genres;
        private ICollection<Photo> gallery;
        private ICollection<Cinema> cinemas;
        private ICollection<Article> relatedArticles;
        private ICollection<Vote> votes;

        public Movie()
        {
            this.characters = new HashSet<Character>();
            this.castMembers = new HashSet<CastMember>();
            this.genres = new HashSet<Genre>();
            this.gallery = new HashSet<Photo>();
            this.cinemas = new HashSet<Cinema>();
            this.relatedArticles = new HashSet<Article>();
            this.votes = new HashSet<Vote>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(60)]
        public string Title { get; set; }

        [Required]
        public string StoryLine { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        //TODO Decide nullable or not
        public DateTime? DVDReleaseDate { get; set; }

        [Required]
        public int Duration { get; set; }

        public string OfficialTrailer { get; set; }

        public virtual MoviePoster Poster { get; set; }

        [Required]
        public virtual ICollection<Genre> Genres
        {
            get
            {
                return this.genres;
            }
            set
            {
                this.genres = value;
            }
        }

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

        public virtual ICollection<Cinema> Cinemas
        {
            get
            {
                return this.cinemas;
            }
            set
            {
                this.cinemas = value;
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

        public virtual ICollection<Vote> Votes
        {
            get
            {
                return this.votes;
            }
            set
            {
                this.votes = value;
            }
        }
    }
}
