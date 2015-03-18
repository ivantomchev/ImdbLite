namespace ImdbLite.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using ImdbLite.Data.Models;

    public interface IApplicationDbContext
    {
        IDbSet<Celebrity> Celebrities { get; set; }

        IDbSet<Movie> Movies { get; set; }

        IDbSet<Genre> Genres { get; set; }

        IDbSet<Character> Characters { get; set; }

        IDbSet<CastMember> CastMembers { get; set; }

        IDbSet<CelebrityMainPhoto> CelebrityMainPhotos { get; set; }

        IDbSet<MoviePoster> MoviePosters { get; set; }

        IDbSet<Photo> Photos { get; set; }

        IDbSet<Cinema> Cinemas { get; set; }

        IDbSet<City> Cities { get; set; }

        IDbSet<Contact> Contacts { get; set; }

        IDbSet<Article> Articles { get; set; }

        IDbSet<ArticlePhoto> ArticlePhotos { get; set; }

        int SaveChanges();

        void Dispose();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}
