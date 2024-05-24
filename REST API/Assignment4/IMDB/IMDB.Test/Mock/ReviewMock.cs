using IMDB.Domain.Model;
using IMDB.Repository.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Linq;


namespace IMDB.Test.Mock
{
    public class ReviewMock
    {
        public static readonly Mock<IReviewRepository> MockReviewRepo = new Mock<IReviewRepository>();
        public static List<Review> ListOfReviews = new List<Review>()
        {
            new Review
            {
                Id = 1,
                Message = "Dummy",
                MovieId = 1,
            },
            new Review
            {
                Id = 2,
                Message = "Dummy2",
                MovieId = 1,

            },
            new Review
            {
                Id = 3,
                Message = "Dummy3",
                MovieId = 1,
            }
        };
        public static void MockCreate()
        {
            MockReviewRepo.Setup(x => x.Create(It.IsAny<Review>())).ReturnsAsync(ListOfReviews.Max(x => x.Id) + 1);
        }
        public static void MockGetAll()
        {
            MockReviewRepo.Setup(x => x.Get()).ReturnsAsync(ListOfReviews);
        }

        public static void MockGetById()
        {
            MockReviewRepo.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((int id) => ListOfReviews.FirstOrDefault(x => x.Id==id));
        }

        public static void MockUpdate()
        {
            MockReviewRepo.Setup(x => x.Update(It.IsAny<Review>()));
        }

        public static void MockDelete()
        {
            MockReviewRepo.Setup(x => x.Delete(It.IsAny<int>()));
        }

        public static void MockGetByMovieId()
        {
            MockReviewRepo.Setup(x=>x.GetByMovieId(It.IsAny<int>())).ReturnsAsync((int id)=>ListOfReviews.Where(x=>x.MovieId==id));
        }
    }
}
