using IMDB.Services.Interfaces;
using IMDB.Services;
using IMDB.Test.Mock;
using TechTalk.SpecFlow;
using Microsoft.Extensions.DependencyInjection;

namespace IMDB.Test.StepDefinitions
{
    [Scope(Feature = "Producer")]
    [Binding]
    public class ProducerSteps:BaseSteps
    {
        public ProducerSteps(CustomWebApplicationFactory<TestStartup> factory)
            : base(factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddScoped(_ => ProducerMock.MockProducerRepo.Object);
                    services.AddScoped<IProducerService, ProducerService>();
                });
            }))
        {

        }
        [BeforeScenario]
        [Scope(Tag = "CreateProducer")]
        public static void MockCreate()
        {
            ProducerMock.MockCreate();
        }
        [BeforeScenario]
        [Scope(Tag = "GetAllProducers")]
        public static void MockGetAll()
        {
            ProducerMock.MockGetAll();
        }
        [BeforeScenario]
        [Scope(Tag = "GetProducerById")]
        public static void MockGetById()
        {
            ProducerMock.MockGetById();
        }
        [BeforeScenario]
        [Scope(Tag = "UpdateProducer")]
        public static void MockUpdateProducer()
        {
            ProducerMock.MockUpdate();
        }

        [BeforeScenario]
        [Scope(Tag = "DeleteProducer")]
        public static void MockDeleteProducer()
        {
            ProducerMock.MockDelete();
        }
        
    }
}