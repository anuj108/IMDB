using IMDB.Domain.Model;
using IMDB.Repository.Interfaces;
using IMDB.Test.Mock.Mapping;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IMDB.Test.Mock.ActorMock;

namespace IMDB.Test.Mock
{
    public class GenreMock
    {
        public static readonly Mock<IGenreRepository> MockGenreRepo = new Mock<IGenreRepository>();
        public static List<Genre> GenreList = new List<Genre>()
        {
            new Genre
            {
                Id = 1,
                Name = "Dummy",
                
            },
            new Genre
            {
                Id = 2,
                Name = "Dummy2",
                
            },
            new Genre
            {
                Id = 3,
                Name = "Dummy3",
                
            }
        };

        public static List<Genres_Movies> GenresMovies = new List<Genres_Movies>()
        {
            new Genres_Movies
            {
                genreId=1,
                movieId=1
            }
        };
        public static void MockCreate()
        {
            MockGenreRepo.Setup(x => x.Create(It.IsAny<Genre>())).ReturnsAsync(GenreList.Max(x => x.Id) + 1);
        }
        public static void MockGetAll()
        {
            MockGenreRepo.Setup(x => x.Get()).ReturnsAsync(GenreList);
        }

        public static void MockGetById()
        {
            MockGenreRepo.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((int id) => GenreList.FirstOrDefault(x => x.Id==id));
        }

        public static void MockUpdate()
        {
            MockGenreRepo.Setup(x => x.Update(It.IsAny<Genre>()));
        }

        public static void MockDelete()
        {
            MockGenreRepo.Setup(x => x.Delete(It.IsAny<int>()));
        }
        public static void MockGetGenresForMovie()
        {
            MockGenreRepo.Setup(x => x.GetGenresForMovie(It.IsAny<int>())).ReturnsAsync((int id) =>
            {
                var actorIds = GenresMovies.Where(y => y.movieId==id).Select(y => y.genreId);
                return GenreList.Where(y => actorIds.Contains(y.Id));
            });
        }
    }
}
