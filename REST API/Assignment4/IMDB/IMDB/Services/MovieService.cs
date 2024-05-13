using IMDB.Repository.Interfaces;
using IMDB.Repository;
using IMDB.Services.Interfaces;
using IMDB.Domain.Model;
using IMDB.Domain.Request;
using System.Security.Cryptography;
using IMDB.Domain.Response;
using System.Xml.Linq;
using IMDB.CustomExceptions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace IMDB.Services
{
    public class MovieService:IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IActorService _actorService;
        private readonly IProducerService _producerService;
        private readonly IGenreService _genreService;
        private int _id = 0;
        public MovieService(IMovieRepository movieRepository, IActorService actorService, IProducerService producerService, IGenreService genreService)
        {
            _movieRepository = movieRepository;
            _actorService=actorService;
            _producerService=producerService;
            _genreService=genreService;
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



            
            return await _movieRepository.Create(new Movie
            {
                Name = movieRequest.Name,
                YearOfRelease = movieRequest.YearOfRelease,
                Plot = movieRequest.Plot,
                
                Producer = movieRequest.ProducerId,
                CoverImage = movieRequest.CoverImage
            }, string.Join(',', movieRequest.ActorIds),
                string.Join(',', movieRequest.GenreIds));
        }

        public async Task<IEnumerable<MovieResponse>> Get()
        {
            var movieResponse=await _movieRepository.Get();
            if(!movieResponse.Any()) throw new NotFoundException("NO MOVIE FOUND");
           

            return movieResponse.Select(x=>new MovieResponse
            {
                Id=x.Id,
                Name = x.Name,
                YearOfRelease = x.YearOfRelease,
                Plot = x.Plot,
                Actors=_actorService.GetActorsForMovie(x.Id).Result,
                Genres=_genreService.GetGenresForMovie(x.Id).Result,
                Producer=_producerService.Get(x.Id).Result,
                CoverImage=x.CoverImage
            });
        }

        public async Task<MovieResponse> Get(int id)
        {
            var movieData = await _movieRepository.Get(id);
            if (movieData==null) throw new NotFoundException("NO MOVIE FOUND");
           
            
            return new MovieResponse
            {
                Id=id,
                Name = movieData.Name,
                YearOfRelease=movieData.YearOfRelease,
                Plot = movieData.Plot,

                Actors=_actorService.GetActorsForMovie(id).Result,
                Genres=_genreService.GetGenresForMovie(id).Result,
                Producer=_producerService.Get(id).Result,
                CoverImage=movieData.CoverImage
            };
        }

        public async Task<IEnumerable<MovieResponse>> GetByYear(int year) {
            if (year < 1800 || year > DateTime.Now.Year + 10) throw new BadRequestException("INVALID YEAR"); 
            var movieData= await _movieRepository.GetByYear(year);

            return movieData.Select(x => new MovieResponse
            {
                Id = x.Id,
                Name= x.Name,
                YearOfRelease=x.YearOfRelease,
                Plot = x.Plot,
                Actors=_actorService.GetActorsForMovie(x.Id).Result,
                Genres=_genreService.GetGenresForMovie(x.Id).Result,
                Producer=_producerService.Get(x.Id).Result,
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
                Producer = movieRequest.ProducerId,
                CoverImage=movieRequest.CoverImage
            }, string.Join(',', movieRequest.ActorIds),
                string.Join(',', movieRequest.GenreIds));

        }
        public async Task Delete(int id) {
            if ((await _movieRepository.Get(id)) == null) throw new NotFoundException("NO MOVIE FOUND");
            await _movieRepository.Delete(id);
        }
    }
}
