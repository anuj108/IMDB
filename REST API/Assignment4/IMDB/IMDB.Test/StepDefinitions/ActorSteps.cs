using IMDB.Services;
using IMDB.Services.Interfaces;
using IMDB.Test.StepDefinitions;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using TechTalk.SpecFlow;
using Microsoft.Extensions.DependencyInjection;
using IMDB.Test.Mock;
using TechTalk.SpecFlow.Infrastructure;

namespace IMDB.Test
{
    [Scope(Feature = "Actor")]
    [Binding]
    public class ActorSteps : BaseSteps
    {
        
        public ActorSteps(CustomWebApplicationFactory<TestStartup> factory)
            : base(factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                 services.AddScoped(_ => ActorMock.MockActorRepo.Object);
                    services.AddScoped<IActorService, ActorService>();
                });
            }))
        {
            
        }
        [BeforeScenario]
        [Scope(Tag = "CreateActor")]
        public static void MockCreate()
        {
            ActorMock.MockCreate();
        }
        [BeforeScenario]
        [Scope(Tag = "GetAllActors")]
        public static void MockGetAll()
        {
            ActorMock.MockGetAll();
        }
        [BeforeScenario]
        [Scope(Tag = "GetActorById")]
        public static void MockGetById()
        {
            ActorMock.MockGetById();
        }
        [BeforeScenario]
        [Scope(Tag = "UpdateActor")]
        public static void MockUpdateActor()
        {
            ActorMock.MockUpdate();
        }

        [BeforeScenario]
        [Scope(Tag = "DeleteActor")]
        public static void MockDeleteActor()
        {
            ActorMock.MockDelete();
        }

        public static void MockGetActorsForMovie()
        {
            ActorMock.MockGetActorsForMovie();
        }
    }
}
