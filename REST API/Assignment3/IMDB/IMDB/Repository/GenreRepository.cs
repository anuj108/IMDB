using IMDB.Domain.Model;
using IMDB.Repository.Interfaces;
using Microsoft.Extensions.Options;

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
            const string query = @"Insert into Foundation.Genres([Name]) values(@name) Select Scope_Identity()";
            return await Create(query,
                new
                {
                    Name=name
                });
        }

        public async Task<IEnumerable<Genre>> Get()
        {
            const string query = @"SELECT * FROM Foundation.Genres";
            return await Get(query);
        }
        public async Task<Genre> Get(int id)
        {
            const string query = @"Select * from foundation.Genres where [Id]=@id";
            return await Get(query, new { Id = id });
        }

        public async Task Update(Genre genre)
        {
            var name=genre.Name;
            const string query = @"UPDATE FOUNDATION.Genres
SET [Name] = @name
WHERE [Id] = @id";
            await Update(query, new
            {
                Name = name
            });
        }

        public async Task Delete(int id)
        {
            const string query = @"Delete from FOUNDATION.Genres where [Id]=@id";
            await Delete(query, new { Id = id });
        }
    }
}
