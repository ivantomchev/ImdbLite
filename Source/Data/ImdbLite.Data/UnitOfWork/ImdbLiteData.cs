namespace ImdbLite.Data.UnitOfWork
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ImdbLite.Data.Common.Models;
    using ImdbLite.Data.Common.Repository;
    using ImdbLite.Data.Models;

    public class ImdbLiteData : IImdbLiteData
    {
        private readonly IApplicationDbContext _context;

        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public ImdbLiteData(IApplicationDbContext context) => this._context = context;

        public IApplicationDbContext Context => this._context;

        public IDeletableEntityRepository<User> Users => this.GetDeletableEntityRepository<User>();

        public IDeletableEntityRepository<Celebrity> Celebrities => this.GetDeletableEntityRepository<Celebrity>();

        public IDeletableEntityRepository<Movie> Movies => this.GetDeletableEntityRepository<Movie>();

        public IDeletableEntityRepository<Genre> Genres => this.GetDeletableEntityRepository<Genre>();

        public IDeletableEntityRepository<Character> Characters => this.GetDeletableEntityRepository<Character>();

        public IDeletableEntityRepository<CastMember> CastMembers => this.GetDeletableEntityRepository<CastMember>();

        public IDeletableEntityRepository<Photo> Gallery => this.GetDeletableEntityRepository<Photo>();

        public IDeletableEntityRepository<Cinema> Cinemas => this.GetDeletableEntityRepository<Cinema>();

        public IDeletableEntityRepository<City> Cities => this.GetDeletableEntityRepository<City>();

        public IDeletableEntityRepository<Contact> Contacts => this.GetDeletableEntityRepository<Contact>();

        public IDeletableEntityRepository<Article> Articles => this.GetDeletableEntityRepository<Article>();

        public IDeletableEntityRepository<Vote> Votes => this.GetDeletableEntityRepository<Vote>();

        public int SaveChanges() => this._context.SaveChanges();

        public async Task<int> SaveChangesAsync() => await this.Context.SaveChangesAsync();

        public void Dispose() => this.Dispose(true);

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this._context != null)
                {
                    this._context.Dispose();
                }
            }
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(GenericRepository<T>);
                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this._context));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }

        private IDeletableEntityRepository<T> GetDeletableEntityRepository<T>() where T : class, IDeletableEntity
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(DeletableEntityRepository<T>);
                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this._context));
            }

            return (IDeletableEntityRepository<T>)this.repositories[typeof(T)];
        }
    }
}
