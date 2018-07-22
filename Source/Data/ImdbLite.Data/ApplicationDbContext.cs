namespace ImdbLite.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using ImdbLite.Data.Common.Models;
    using ImdbLite.Data.Migrations;
    using ImdbLite.Data.Models;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationDbContext : IdentityDbContext<User>, IApplicationDbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
            => Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());

        public static ApplicationDbContext Create() 
            => new ApplicationDbContext();

        public virtual IDbSet<Celebrity> Celebrities { get; set; }

        public virtual IDbSet<Movie> Movies { get; set; }

        public virtual IDbSet<Genre> Genres { get; set; }

        public virtual IDbSet<Character> Characters { get; set; }

        public virtual IDbSet<CastMember> CastMembers { get; set; }

        public virtual IDbSet<CelebrityMainPhoto> CelebrityMainPhotos { get; set; }

        public virtual IDbSet<MoviePoster> MoviePosters { get; set; }

        public virtual IDbSet<Photo> Photos { get; set; }

        public virtual IDbSet<Cinema> Cinemas { get; set; }

        public virtual IDbSet<City> Cities { get; set; }

        public virtual IDbSet<Contact> Contacts { get; set; }

        public virtual IDbSet<Article> Articles { get; set; }

        public virtual IDbSet<ArticlePhoto> ArticlePhotos { get; set; }

        public virtual IDbSet<Vote> Votes { get; set; }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync()
        {
            this.ApplyAuditInfoRules();
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CelebrityMainPhoto>().HasKey(x => x.CelebrityId);
            modelBuilder.Entity<Celebrity>().HasRequired(x => x.Photo).WithRequiredPrincipal(x => x.Celebrity).WillCascadeOnDelete(true);

            modelBuilder.Entity<MoviePoster>().HasKey(x => x.MovieId);
            modelBuilder.Entity<Movie>().HasRequired(x => x.Poster).WithRequiredPrincipal(x => x.Movie).WillCascadeOnDelete(true);

            modelBuilder.Entity<ArticlePhoto>().HasKey(x => x.ArticleId);
            modelBuilder.Entity<Article>().HasRequired(x => x.Photo).WithRequiredPrincipal(x => x.Article).WillCascadeOnDelete(true);

            base.OnModelCreating(modelBuilder);
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            var entries = this.ChangeTracker
                    .Entries()
                    .Where(e => e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified)));

            foreach (var entry in entries)
            {
                var entity = (IAuditInfo)entry.Entity;

                if (entry.State == EntityState.Added && !entity.PreserveCreatedOn)
                {
                    entity.CreatedOn = DateTime.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }

        public new IDbSet<T> Set<T>() where T : class 
            => base.Set<T>();

        protected override void Dispose(bool disposing) 
            => base.Dispose(disposing);
    }
}
