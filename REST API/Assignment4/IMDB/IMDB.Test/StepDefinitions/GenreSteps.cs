using IMDB.Services.Interfaces;
using IMDB.Services;
using IMDB.Test.Mock;
using System;
using TechTalk.SpecFlow;
using Microsoft.Extensions.DependencyInjection;

namespace IMDB.Test.StepDefinitions
{
    [Binding]
    [Scope(Feature="Genre")]
    public class GenreSteps:BaseSteps
    {
        public GenreSteps(CustomWebApplicationFactory<TestStartup> factory)
             : base(factory.WithWebHostBuilder(builder =>
             {
                 builder.ConfigureServices(services =>
                 {
                     services.AddScoped(_ => GenreMock.MockGenreRepo.Object);
                     services.AddScoped<IGenreService, GenreService>();
                 });
             }))
        {

        }
        [BeforeScenario]
        [Scope(Tag = "CreateGenre")]
        public static void MockCreate()
        {
            GenreMock.MockCreate();
        }
        [BeforeScenario]
        [Scope(Tag = "GetAllGenres")]
        public static void MockGetAll()
        {
            GenreMock.MockGetAll();
        }
        [BeforeScenario]
        [Scope(Tag = "GetGenreById")]
        public static void MockGetById()
        {
            GenreMock.MockGetById();
        }
        [BeforeScenario]
        [Scope(Tag = "UpdateGenre")]
        public static void MockUpdateGenre()
        {
            GenreMock.MockUpdate();
        }

        [BeforeScenario]
        [Scope(Tag = "DeleteGenre")]
        public static void MockDeleteGenre()
        {
            GenreMock.MockDelete();
        }

        public static void MockGetGenresForMovie()
        {
            GenreMock.MockGetGenresForMovie();
        }
    }
}
