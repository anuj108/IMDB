using IMDB.Services.Interfaces;
using IMDB.Services;
using IMDB.Test.Mock;
using System;
using TechTalk.SpecFlow;
using Microsoft.Extensions.DependencyInjection;

namespace IMDB.Test.StepDefinitions
{
    [Binding]
    [Scope(Feature ="Review")]
    public class ReviewSteps:BaseSteps
    {
        public ReviewSteps(CustomWebApplicationFactory<TestStartup> factory)
              : base(factory.WithWebHostBuilder(builder =>
              {
                  builder.ConfigureServices(services =>
                  {
                      services.AddScoped(_ => ReviewMock.MockReviewRepo.Object);
                      services.AddScoped<IReviewService, ReviewService>();
                      services.AddScoped(_ => MovieMock.MockMovieRepo.Object);

                  });
              }))
        {

        }
        [BeforeScenario]
        [Scope(Tag = "CreateReview")]
        public static void MockCreate()
        {
            ReviewMock.MockCreate();
        }

        [BeforeScenario]
        [Scope(Tag = "CreateReview")]
        public static void MockGetMovie()
        {
            MovieMock.MockGetAll();
        }

        [BeforeScenario]
        [Scope(Tag = "GetReviewById")]
        [Scope(Tag = "GetAllReviews")]
        [Scope(Tag = "CreateReview")]
        public static void MockGetAll()
        {
            ReviewMock.MockGetAll();
        }
        [BeforeScenario]
        [Scope(Tag = "GetReviewById")]
        [Scope(Tag = "DeleteReview")]
        public static void MockGetById()
        {
            ReviewMock.MockGetById();
        }

        [BeforeScenario]
        [Scope(Tag = "UpdateReview")]
        public static void MockUpdate()
        {
            ReviewMock.MockUpdate();
        }

        [BeforeScenario]
        [Scope(Tag = "DeleteReview")]
        public static void MockDeleteReview()
        {
            ReviewMock.MockDelete();
        }
    }
}
