using IMDB.Services.Interfaces;
using IMDB.Services;
using IMDB.Test.Mock;
using System;
using TechTalk.SpecFlow;
using Microsoft.Extensions.DependencyInjection;

namespace IMDB.Test.StepDefinitions
{
    [Binding]
    public class MovieSteps:BaseSteps
    {
        public MovieSteps(CustomWebApplicationFactory<TestStartup> factory)
           : base(factory.WithWebHostBuilder(builder =>
           {
               builder.ConfigureServices(services =>
               {
                   services.AddScoped(_ => MovieMock.MockMovieRepo.Object);
                   services.AddScoped(_ => ActorMock.MockActorRepo.Object);
                   services.AddScoped(_ => ProducerMock.MockProducerRepo.Object);
                   services.AddScoped(_ => GenreMock.MockGenreRepo.Object);
                   services.AddScoped<IMovieService, MovieService>();
                   services.AddScoped<IActorService, ActorService>();
                   services.AddScoped<IGenreService, GenreService>();
                   services.AddScoped<IProducerService, ProducerService>();
               });
           }))
        {

        }
        [BeforeScenario]
        [Scope(Tag = "CreateMovie")]
        public static void MockCreate()
        {
            MovieMock.MockCreate();
        }
        [BeforeScenario]
        [Scope(Tag = "GetAllMovies")]
        [Scope(Tag = "GetMovieById")]
        public static void MockGetAll()
        {
            MovieMock.MockGetAll();
        }
        [BeforeScenario]
        [Scope(Tag = "GetAllMovies")]
        [Scope(Tag = "GetMovieById")]
        [Scope(Tag = "GetMoviesByYear")]
        public static void MockGetAllActors()
        {
            ActorMock.MockGetAll();
        }
        [BeforeScenario]
        [Scope(Tag = "GetAllMovies")]
        public static void MockGetAllProducer()
        {
            ProducerMock.MockGetAll();
        }
        [BeforeScenario]
        [Scope(Tag = "GetMovieById")]
        [Scope(Tag = "GetAllMovies")]
        [Scope(Tag = "GetMoviesByYear")]
        public static void MockGetByIdProducer()
        {
            ProducerMock.MockGetById();
        }
        [BeforeScenario]
        [Scope(Tag="GetMovieById")]
        [Scope(Tag = "GetAllMovies")]
        public static void MockGetAllGenre()
        {
            GenreMock.MockGetAll();
        }
        [BeforeScenario]
        [Scope(Tag = "GetMovieById")]
        [Scope(Tag = "DeleteMovie")]
        [Scope(Tag = "UpdateMovie")]
        public static void MockGetById()
        {
            MovieMock.MockGetById();
        }
        [BeforeScenario]
        [Scope(Tag = "Updatemovie")]
        public static void MockUpdatemovie()
        {
            MovieMock.MockUpdate();
        }

        [BeforeScenario]
        [Scope(Tag = "DeleteMovie")]
        [Scope(Tag = "GetMovieById")]
        public static void MockDeletemovie()
        {
            MovieMock.MockDelete();
        }

        [BeforeScenario]
        [Scope(Tag = "GetMovieById")]
        [Scope(Tag = "GetAllMovies")]
        [Scope(Tag = "GetMoviesByYear")]
        public static void MockGetActorsForMovie()
        {
            ActorMock.MockGetActorsForMovie();
        }

        [BeforeScenario]
        [Scope(Tag = "GetAllMovies")]
        [Scope(Tag = "GetMovieById")]
        [Scope(Tag = "GetMoviesByYear")]
        public static void MockGetGenresForMovie()
        {
            GenreMock.MockGetGenresForMovie();
        }

        [Scope(Tag = "GetAllMovies")]
        [Scope(Tag = "GetMovieById")]
        [Scope(Tag = "GetMoviesByYear")]
        [BeforeScenario]
        public static void MockGetMoviesByYear()
        {
            MovieMock.MockGetMoviesByYear();
        }
    }
}
