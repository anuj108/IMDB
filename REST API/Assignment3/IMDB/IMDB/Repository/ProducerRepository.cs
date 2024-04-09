
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
            const string query = @"Insert into foundation.producers([Name],[Sex],[DOB],[Bio] values(@name,@gender,@dob,@bio))
Select Scope_Identity()";
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
            const string query = @"Select [id],[Name],[Sex],[DOB],[Bio] from FOUNDATION.Producers";
            return await Get(query);
        }

        public async Task<Producer> Get(int id)
        {
            const string query = @"Select [id],[Name],[Sex],[DOB],[Bio] from FOUNDATION.Producers where [id]=@id";
            return await Get(query, new {Id=id});
        }

        public async Task Update(Producer producer)
        {

            var id=producer.Id;
            var name = producer.Name;
            var gender= producer.Gender;
            var dob = producer.DOB;
            var bio= producer.Bio;
            const string query = @"Update Foundation.Producers SET [name]=@name,[Sex]=@gender,[DOB]=@dob,[Bio]=bio where [Id]=@id";
            await Get(query, new
            {
                Name=name,
                Gender=gender,
                DOB=dob,
                Bio=bio
            });
        }

        public async Task Delete(int id)
        {
            const string query = @"Delete from Foundation.Producers where [Id]=@id";
            await Delete(query, new {Id=id});
        }
    }
}
