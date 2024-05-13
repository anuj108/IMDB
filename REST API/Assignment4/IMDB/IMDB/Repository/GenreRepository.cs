using IMDB.Domain.Model;
using IMDB.Repository.Interfaces;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB.Repository
{
    public class GenreRepository:BaseRepository<Genre>,IGenreRepository
    {
        private readonly ConnectionString _connectionString;
        public GenreRepository(IOptions<ConnectionString> connectionString)
            :base(connectionString.Value.IMDBDB)
        {
            _connectionString=connectionString.Value;
        }
 
        public async Task<int> Create(Genre genre)
        {
            var name = genre.Name;
            const string query = @"INSERT INTO Foundation.Genres ([Name])
VALUES (@name)

SELECT Scope_Identity()";
            return await Create(query,
                new
                {
                    Name=name
                });
        }

        public async Task<IEnumerable<Genre>> Get()
        {
            const string query = @"SELECT [Id],[Name] FROM Foundation.Genres";
            return await Get(query);
        }
        public async Task<Genre> Get(int id)
        {
            const string query = @"Select [Id],[Name] from foundation.Genres where [Id]=@id";
            return await Get(query, new { Id = id });
        }

        public async Task Update(Genre genre)
        {
            var name=genre.Name;
            var id = genre.Id;
            const string query = @"UPDATE FOUNDATION.Genres
SET [Name] = @Name
WHERE [Id] = @Id";
            await Update(query, new
            {
                Name = name,
                Id=id
            });
        }

        public async Task Delete(int id)
        {
            const string query = @"EXEC Foundation.usp_Delete_Genre @Id = @Id";
            await Delete(query, new { Id = id });
        }

        public async Task<IEnumerable<Genre>> GetGenresForMovie(int id)
        {
            const string query = @"SELECT [Id],[Name]
FROM Foundation.Genres G
INNER JOIN Foundation.Genres_Movies GM ON G.id = GM.genreId
WHERE movieId = @id";
            return await GetForMovie(query, new { Id = id });
        }
    }
}
