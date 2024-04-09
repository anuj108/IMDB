using Firebase.Storage;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using IMDB.Domain.Model;
using IMDB.Repository.Interfaces;
using Microsoft.Extensions.Options;

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
       

        public async Task<int> Create(Movie movie)
        {
            var name=movie.Name;
            var yor = movie.YearOfRelease;
            var plot = movie.Plot;
            var producer=movie.Producer.Id;
            var actors = string.Join(",",movie.Actors.Select(x => x.Id));
            var genres = string.Join(",", movie.Genres.Select(x=>x.Id));
            var coverImage=movie.CoverImage;
            const string query = @"
EXEC [usp_Insert_Movie]
	@Name = @Name,
	@YearofRelease = @YearOfRelease,
	@Plot = @Plot,
	@CoverImage = @CoverImage,
	@ProducerId = @Producer,
	@ActorIds = @Actors,
	@GenreIds = @Genres
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
			catch { }

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
            return [new Movie(), new Movie()];
        }

        public async Task<Movie> Get(int id)
        {
            return new Movie();
            //return _movieRepository.FirstOrDefault(movie=>movie.Id==id);
        }

        public async Task<IEnumerable<Movie>> GetByYear(int year)
        {
            return [new Movie(),new Movie()];
        }

        public async Task Update(Movie movie)
        {
            
        }

        public async Task Delete(int id)
        {
            
        }
    }
}
