namespace ImdbLite.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using ImdbLite.Common;
    using ImdbLite.Common.Extensions;
    using ImdbLite.Data.Models;
    using ImdbLite.Data.UnitOfWork;
    using ImdbLite.Services.Data.DTOs;
    using ImdbLite.Services.Data.Interfaces;

    public class MoviesService : IMoviesService
    {
        private readonly IImdbLiteData _data;

        public MoviesService(IImdbLiteData data)
            => _data = data;

        public async Task<int> GetCountAsync()
            => await _data.Movies.All().CountAsync();

        public async Task<List<MovieListItemDTO>> GetAsync(uint skip = 0, uint take = int.MaxValue, string sort = null)
        {
            var query = _data.Movies
                            .All()
                            .Select(m => new MovieListItemDTO
                            {
                                Id = m.Id,
                                Title = m.Title,
                                ReleaseDate = m.ReleaseDate,
                                CreatedOn = m.CreatedOn,
                                DeletedOn = m.DeletedOn,
                                ModifiedOn = m.ModifiedOn,
                                IsDeleted = m.IsDeleted
                            });

            query = String.Equals(sort, GlobalConstants.ASC, StringComparison.InvariantCultureIgnoreCase)
                                ? query.OrderBy(m => m.CreatedOn)
                                : query.OrderByDescending(m => m.CreatedOn);

            return await query
                            .Skip(skip.ToInt())
                            .Take(take.ToInt())
                            .ToListAsync();
        }

        public async Task<MovieDTO> GetByIdAsync(int id)
        {
            return await _data.Movies
                            .All()
                            .Where(m => m.Id == id)
                            .Select(m => new MovieDTO
                            {
                                Id = m.Id,
                                Title = m.Title,
                                CreatedOn = m.CreatedOn,
                                DeletedOn = m.DeletedOn,
                                ModifiedOn = m.ModifiedOn,
                                IsDeleted = m.IsDeleted,
                                Poster = new FileDTO { Content = m.Poster.Content, FileExtension = m.Poster.FileExtension, Type = m.Poster.Type },
                                OfficialTrailer = m.OfficialTrailer,
                                Duration = m.Duration,
                                DVDReleaseDate = m.DVDReleaseDate,
                                ReleaseDate = m.ReleaseDate,
                                StoryLine = m.StoryLine,
                                Genres = m.Genres.Select(g => g.Id),
                                Cinemas = m.Cinemas.Select(c => c.Id),
                                Directors = m.CastMembers.Where(c => c.Participation == ParticipationType.Director).Select(c => c.CelebrityId),
                                Producers = m.CastMembers.Where(c => c.Participation == ParticipationType.Producer).Select(c => c.CelebrityId),
                                Writers = m.CastMembers.Where(c => c.Participation == ParticipationType.Writer).Select(c => c.CelebrityId),
                                Characters = m.Characters.Select(c => new CharacterDTO
                                {
                                    Id = c.Id,
                                    Name = c.CharacterName,
                                    CelebrityName = c.Celebrity.FirstName + " " + c.Celebrity.LastName,
                                    CelebrityId = c.CelebrityId,
                                    MovieId = c.MovieId
                                }),
                            })
                            .SingleAsync();
        }

        public async Task<MovieDTO> AddAsync(MovieDTO movie)
        {
            var dbModel = new Movie();
            dbModel.Title = movie.Title;
            dbModel.StoryLine = movie.StoryLine;
            dbModel.Duration = movie.Duration;
            dbModel.ReleaseDate = movie.ReleaseDate;
            dbModel.DVDReleaseDate = movie.DVDReleaseDate;
            dbModel.OfficialTrailer = movie.OfficialTrailer;
            dbModel.Poster = new MoviePoster { Content = movie.Poster.Content, FileExtension = movie.Poster.FileExtension, Type = movie.Poster.Type };

            movie.Writers.ForEach(id =>
            {
                dbModel.CastMembers.Add(new CastMember { Participation = ParticipationType.Writer, CelebrityId = id });
            });
            movie.Producers.ForEach(id =>
            {
                dbModel.CastMembers.Add(new CastMember { Participation = ParticipationType.Producer, CelebrityId = id });
            });
            movie.Directors.ForEach(id =>
            {
                dbModel.CastMembers.Add(new CastMember { Participation = ParticipationType.Director, CelebrityId = id });
            });
            movie.Genres.ForEach(id =>
            {
                dbModel.Genres.Add(_data.Context.Genres.Attach(new Genre { Id = id }));
            });
            movie.Cinemas.ForEach(id =>
            {
                dbModel.Cinemas.Add(_data.Context.Cinemas.Attach(new Cinema { Id = id }));
            });
            movie.Characters.ForEach(c =>
            {
                dbModel.Characters.Add(new Character { CharacterName = c.Name, CelebrityId = c.CelebrityId });
            });

            var result = _data.Movies.Add(dbModel);
            await _data.SaveChangesAsync();
            movie.Id = result.Id;

            return movie;
        }

        public async Task UpdateAsync(MovieDTO movie)
        {
            var dbModel = _data.Movies.GetById(movie.Id);
            dbModel.Title = movie.Title;
            dbModel.StoryLine = movie.StoryLine;
            dbModel.Duration = movie.Duration;
            dbModel.ReleaseDate = movie.ReleaseDate;
            dbModel.DVDReleaseDate = movie.DVDReleaseDate;
            dbModel.OfficialTrailer = movie.OfficialTrailer;
            dbModel.Poster.Content = movie.Poster.Content;
            dbModel.Poster.Type = movie.Poster.Type;
            dbModel.Poster.FileExtension = movie.Poster.FileExtension;

            var allCastMembers = dbModel.CastMembers.ToList();
            var allCharacters = dbModel.Characters.ToList();

            var directors = allCastMembers
                .Where(c => c.Participation == ParticipationType.Director);
            var producers = allCastMembers
                .Where(c => c.Participation == ParticipationType.Producer);
            var writers = allCastMembers
                .Where(c => c.Participation == ParticipationType.Writer);

            directors
                .Where(c => !movie.Directors.Contains(c.CelebrityId))
                .ForEach(c => { _data.CastMembers.ActualDelete(c); });

            producers
                .Where(c => !movie.Producers.Contains(c.CelebrityId))
                .ForEach(c => { _data.CastMembers.ActualDelete(c); });

            writers
                .Where(c => !movie.Writers.Contains(c.CelebrityId))
                .ForEach(c => { _data.CastMembers.ActualDelete(c); });

            allCharacters
                .Where(c => !movie.Characters.Select(x => x.Id).Contains(c.Id))
                .ForEach(c => { _data.Characters.ActualDelete(c); });

            movie.Directors
                .Where(c => !directors.Select(d => d.CelebrityId).Contains(c))
                .Select(c => new CastMember { Participation = ParticipationType.Director, CelebrityId = c, MovieId = movie.Id })
                .ForEach(c => { _data.CastMembers.Add(c); });

            movie.Producers
                .Where(c => !producers.Select(d => d.CelebrityId).Contains(c))
                .Select(c => new CastMember { Participation = ParticipationType.Producer, CelebrityId = c, MovieId = movie.Id })
                .ForEach(c => { _data.CastMembers.Add(c); });

            movie.Writers
                .Where(c => !writers.Select(d => d.CelebrityId).Contains(c))
                .Select(c => new CastMember { Participation = ParticipationType.Writer, CelebrityId = c, MovieId = movie.Id })
                .ForEach(c => { _data.CastMembers.Add(c); });

            movie.Characters
                .ForEach(c =>
                {
                    var dbCharacter = allCharacters.FirstOrDefault(x => x.Id == c.Id);
                    if (dbCharacter == null)
                    {
                        dbModel.Characters.Add(new Character { CelebrityId = c.CelebrityId, CharacterName = c.Name });
                    }
                    else
                    {
                        dbCharacter.CelebrityId = c.CelebrityId;
                        dbCharacter.CharacterName = c.Name;
                    }
                });

            _data.Movies.Update(dbModel);
            await _data.SaveChangesAsync();
        }
    }
}
