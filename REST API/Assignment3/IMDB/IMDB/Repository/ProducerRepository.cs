
using IMDB.Domain.Model;
using IMDB.Repository.Interfaces;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Xml.Linq;

namespace IMDB.Repository
{
    public class ProducerRepository:BaseRepository<Producer>,IProducerRepository
    {
        private readonly ConnectionString _connectionString;
        public ProducerRepository(IOptions<ConnectionString> connectionString)
        :base(connectionString.Value.IMDBDB)
        {
            _connectionString=connectionString.Value;
        }

        public async Task<int> Create(Producer producer)
        {
            var name=producer.Name;
            var bio=producer.Bio;
            var gender=producer.Gender;
            var dob = producer.DOB;
            const string query = @"INSERT INTO foundation.producers (
	[Name]
	,[Sex]
	,[DOB]
	,[Bio]
	)
VALUES (
	@name
	,@gender
	,@dob
	,@bio
	)

SELECT Scope_Identity()";
            return await Create(query,new
            {
                Name = name,
                Gender = gender,
                DOB = dob,
                Bio = bio
            });
        }

        public async Task<IEnumerable<Producer>> Get()
        {
            const string query = @"SELECT [id]
	,[Name]
	,[Sex] As Gender
	,[DOB]
	,[Bio]
FROM FOUNDATION.Producers";
            return await Get(query);
        }

        public async Task<Producer> Get(int id)
        {
            const string query = @"SELECT [id]
	,[Name]
	,[Sex] As Gender
	,[DOB]
	,[Bio]
FROM FOUNDATION.Producers
WHERE [id] = @id";
            return await Get(query, new {Id=id});
        }

        public async Task Update(Producer producer)
        {

            var id=producer.Id;
            var name = producer.Name;
            var gender= producer.Gender;
            var dob = producer.DOB;
            var bio= producer.Bio;
            const string query = @"UPDATE Foundation.Producers
SET [name] = @Name
	,[Sex] = @Gender
	,[DOB] = @Dob
	,[Bio] = @Bio
WHERE [Id] = @Id";
            await Get(query, new
            {
                Name=name,
                Gender=gender,
                DOB=dob,
                Bio=bio,
                Id=id
            });
        }

        public async Task Delete(int id)
        {
            const string query = @"EXEC Foundation.usp_Delete_Producer @Id = @Id";
            await Delete(query, new {Id=id});
        }
    }
}
