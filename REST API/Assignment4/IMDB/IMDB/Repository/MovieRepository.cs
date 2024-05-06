using Firebase.Storage;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using IMDB.CustomExceptions;
using IMDB.Domain.Model;
using IMDB.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB.Repository
{
    public class MovieRepository:BaseRepository<Movie>,IMovieRepository
    {
        private readonly ConnectionString _connectionString;
        public MovieRepository(IOptions<ConnectionString> connectionString)
            :base(connectionString.Value.IMDBDB)
        {
            _connectionString=connectionString.Value;
        }
       

        public async Task<int> Create(Movie movie,string actorIds,string genreIds)
        {
            var name=movie.Name;
            var yor = movie.YearOfRelease;
            var plot = movie.Plot;
            var producer=movie.Producer;
            var actors = actorIds;
            var genres = genreIds;
            var coverImage=movie.CoverImage;
            const string query = @"
EXEC Foundation.[usp_Insert_Movie] @Name = @Name
	,@YearofRelease = @YearOfRelease
	,@Plot = @Plot
	,@CoverImage = @CoverImage
	,@ProducerId = @Producer
	,@ActorIds = @Actors
	,@GenreIds = @Genres
";
            try
            {
                using var fileStream = File.OpenRead(coverImage);
                var coverImageFile = new FormFile(fileStream, 0, fileStream.Length, null, Path.GetFileName(fileStream.Name));
              
                coverImage = await new FirebaseStorage("imdb-3fcd7.appspot.com")
                        .Child("CoverImages")
                        .Child(Guid.NewGuid().ToString() + ".png")
                        .PutAsync(coverImageFile.OpenReadStream());
            }
			catch(BadRequestException ex) {
                
            }

            return await Create(query, new
            {
                Name = name,
                YearOfRelease = yor,
                Plot = plot,
                Producer = producer,
                Actors = actors,
                Genres = genres,
                CoverImage = coverImage
            });
        }

        /*public async Task<string> UploadImage(IFormFile file)
        {
            var app = FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromApiKey(_connectionString.ApiKey)
            });
        }*/
        
        public async Task<IEnumerable<Movie>> Get() {

            const string query = @"SELECT M.Id
	,M.Name
	,M.YearOfRelease
	,M.Plot
	,M.[ProducerId] AS Producer
	,M.CoverImage
FROM Foundation.Movies M";
            return await Get(query);

        }

        public async Task<Movie> Get(int id)
        {
                const string query = @"SELECT M.Id
	,M.Name
	,M.YearOfRelease
	,M.Plot
	,M.[ProducerId] AS Producer
	,M.CoverImage
FROM Foundation.Movies M
WHERE M.id = @Id";
            return await (Get(query, new {Id=id}));
        }

        public async Task<IEnumerable<Movie>> GetByYear(int year)
        {
            const string query = @"SELECT Id
	,Name
	,YearOfRelease
	,Plot
	,[ProducerId] AS Producer
	,CoverImage
FROM Foundation.Movies";

            var allMovies=await Get(query);

            return allMovies.Where(x => x.YearOfRelease==year);
        }

        public async Task Update(Movie movie, string actorIds, string genreIds)
        {
            var id=movie.Id;
            var name = movie.Name;
            var yearofrelease = movie.YearOfRelease;
            var plot=movie.Plot;
            var producer = movie.Producer;
            var actors = actorIds;
            var genres = genreIds;
            var coverImage=movie.CoverImage;


            const string query = @"EXECUTE Foundation.[usp_Update_Movie] @Id = @Id
	,@Name = @Name
	,@YearofRelease = @Yor
	,@Plot = @Plot
	,@CoverImage = @CoverImage
	,@ProducerId = @Producer
	,@GenreIds = @Genres
	,@ActorIds = @Actors";

            await Update(query, new
            {
                Id=id,
                Name=name,
                Yor=yearofrelease,
                Plot=plot,
                CoverImage=coverImage,
                Actors=actors,
                Genres=genres,
                Producer=producer
            });

        }

        public async Task Delete(int id)
        {
            const string query = @"EXECUTE Foundation.[usp_DELETE_MOVIE] @Id = @Id";
            await Delete(query, new { Id = id });
        }
    }
}
