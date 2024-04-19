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
    }
}
