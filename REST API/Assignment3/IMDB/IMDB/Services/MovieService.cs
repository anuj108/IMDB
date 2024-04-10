using IMDB.Repository.Interfaces;
using IMDB.Repository;
using IMDB.Services.Interfaces;
using IMDB.Domain.Model;
using IMDB.Domain.Request;
using System.Security.Cryptography;
using IMDB.Domain.Response;
using System.Xml.Linq;
using IMDB.CustomExceptions;

namespace IMDB.Services
{
    public class MovieService:IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IActorRepository _actorRepository;
        private readonly IProducerRepository _producerRepository;
        private readonly IGenreRepository _genreRepository;
        private int _id = 0;
        public MovieService(IMovieRepository movieRepository, IActorRepository actorRepository, IProducerRepository producerRepository, IGenreRepository genreRepository)
        {
            _movieRepository = movieRepository;
            _actorRepository=actorRepository;
            _producerRepository=producerRepository;
            _genreRepository=genreRepository;
        }
        public async Task<int> Create(MovieRequest movieRequest)
        {
            if (string.IsNullOrWhiteSpace(movieRequest.Name)) throw new BadRequestException("INVALID MOVIE NAME");
            if (movieRequest.YearOfRelease < 1800 || movieRequest.YearOfRelease > DateTime.Now.Year + 10) throw new BadRequestException("INVALID YEAR"); 
            if(string.IsNullOrWhiteSpace(movieRequest.Plot)) throw new BadRequestException("INVALID MOVIE PLOT");
            if(movieRequest.ActorIds.Count<=0) throw new NotFoundException("ACTORS EMPTY");
            if (movieRequest.GenreIds.Count<=0) throw new NotFoundException("GENRES EMPTY");
            if (movieRequest.ProducerId<=0) throw new NotFoundException("PRODUCER INVALID");
            if (string.IsNullOrWhiteSpace(movieRequest.CoverImage)) throw new BadRequestException("INVALID COVERIMAGE");
            
            var producer =await _producerRepository.Get(movieRequest.ProducerId);
            
            return await _movieRepository.Create(new Movie
            {
                Name = movieRequest.Name,
                YearOfRelease = movieRequest.YearOfRelease,
                Plot = movieRequest.Plot,
                Actors = string.Join(',', movieRequest.ActorIds),
                Genres = string.Join(',', movieRequest.GenreIds),
                Producer = movieRequest.ProducerId,
                CoverImage = movieRequest.CoverImage
            });
        }

        public async Task<IEnumerable<MovieResponse>> Get()
        {
            var movieResponse=await _movieRepository.Get();
            if(!movieResponse.Any()) throw new NotFoundException("NO MOVIE FOUND");
            var actorResponse = await _actorRepository.Get();
            var genreResponse = await _genreRepository.Get();
            var producerResponse = await _producerRepository.Get();
            return movieResponse.Select(x=>new MovieResponse
            {
                Id=x.Id,
                Name = x.Name,
                YearOfRelease = x.YearOfRelease,
                Plot = x.Plot,
                Actors=string.Join(",", actorResponse.Where(y => x.Actors.Split(',').Select(int.Parse).ToList().Contains(y.Id)).Select(z => z.Name)),
                Genres=string.Join(",", genreResponse.Where(y => x.Genres.Split(',').Select(int.Parse).ToList().Contains(y.Id)).Select(z => z.Name)),
                Producer=producerResponse.FirstOrDefault(y => y.Id==x.Producer).Name,
                CoverImage=x.CoverImage
            }).ToList();
        }

        public async Task<MovieResponse> Get(int id)
        {
            var responseData = await _movieRepository.Get(id);
            if (responseData==null) throw new NotFoundException("NO MOVIE FOUND");
            var actorResponse = await _actorRepository.Get();
            var genreResponse = await _genreRepository.Get();
            var listActor = responseData.Actors.Split(',').Select(int.Parse).ToList();
            var listGenre = responseData.Genres.Split(',').Select(int.Parse).ToList();
            return new MovieResponse
            {
                Id=id,
                Name = responseData.Name,
                YearOfRelease=responseData.YearOfRelease,
                Plot = responseData.Plot,
                Actors=string.Join(",",actorResponse.Where(x=>listActor.Contains(x.Id)).Select(x=>x.Name)),
                Genres=string.Join(",", genreResponse.Where(x => listGenre.Contains(x.Id)).Select(x => x.Name)),
                Producer=(await _producerRepository.Get(responseData.Producer)).Name,
                CoverImage=responseData.CoverImage
            };
        }

        public async Task<IEnumerable<MovieResponse>> GetByYear(int year) {
            if (year < 1800 || year > DateTime.Now.Year + 10) throw new BadRequestException("INVALID YEAR"); 
            var responseData= await _movieRepository.GetByYear(year);
            var actorResponse = await _actorRepository.Get();
            var genreResponse = await _genreRepository.Get();
            var producerResponse=await _producerRepository.Get();
            return responseData.Select(x => new MovieResponse
            {
                Id = x.Id,
                Name= x.Name,
                YearOfRelease=x.YearOfRelease,
                Plot = x.Plot,
                Actors=string.Join(",", actorResponse.Where(y => x.Actors.Split(',').Select(int.Parse).ToList().Contains(y.Id)).Select(z => z.Name)),
                Genres=string.Join(",", genreResponse.Where(y => x.Genres.Split(',').Select(int.Parse).ToList().Contains(y.Id)).Select(z => z.Name)),
                Producer=producerResponse.FirstOrDefault(y=>y.Id==x.Producer).Name,
                CoverImage=x.CoverImage
            }).ToList();
        }
        public async Task Update(int id,MovieRequest movieRequest)
        {
            if ((await _movieRepository.Get(id)) == null) throw new NotFoundException("NO MOVIE FOUND");
            if (string.IsNullOrWhiteSpace(movieRequest.Name)) throw new BadRequestException("INVALID MOVIE NAME");
            if (movieRequest.YearOfRelease < 1800 || movieRequest.YearOfRelease > DateTime.Now.Year + 10) throw new BadRequestException("INVALID YEAR");
            if (string.IsNullOrWhiteSpace(movieRequest.Plot)) throw new BadRequestException("INVALID MOVIE PLOT");
            if (movieRequest.ActorIds.Count<=0) throw new NotFoundException("ACTORS EMPTY");
            if (movieRequest.GenreIds.Count<=0) throw new NotFoundException("GENRES EMPTY");
            if (movieRequest.ProducerId<=0) throw new NotFoundException("PRODUCER INVALID");
            if (string.IsNullOrWhiteSpace(movieRequest.CoverImage)) throw new BadRequestException("INVALID COVERIMAGE");
           
            await _movieRepository.Update(new Movie
            {
                Id = id,
                Name=movieRequest.Name,
                YearOfRelease= movieRequest.YearOfRelease,
                Plot= movieRequest.Plot,
                Actors = string.Join(',', movieRequest.ActorIds),
                Genres = string.Join(',', movieRequest.GenreIds),
                Producer = movieRequest.ProducerId,
                CoverImage=movieRequest.CoverImage
            });
            
        }
        public async Task Delete(int id) {
            if ((await _movieRepository.Get(id)) == null) throw new NotFoundException("NO MOVIE FOUND");
            await _movieRepository.Delete(id);
        }
    }
}
