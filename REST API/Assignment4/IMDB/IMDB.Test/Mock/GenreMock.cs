using IMDB.Domain.Model;
using IMDB.Repository.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Test.Mock
{
    public class GenreMock
    {
        public static readonly Mock<IGenreRepository> MockGenreRepo = new Mock<IGenreRepository>();
        public static List<Genre> ListOfGenres = new List<Genre>()
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
        public static void MockCreate()
        {
            MockGenreRepo.Setup(x => x.Create(It.IsAny<Genre>())).ReturnsAsync(ListOfGenres.Max(x => x.Id) + 1);
        }
        public static void MockGetAll()
        {
            MockGenreRepo.Setup(x => x.Get()).ReturnsAsync(ListOfGenres);
        }

        public static void MockGetById()
        {
            MockGenreRepo.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync((int id) => ListOfGenres.FirstOrDefault(x => x.Id==id));
        }

        public static void MockUpdate()
        {
            MockGenreRepo.Setup(x => x.Update(It.IsAny<Genre>()));
        }

        public static void MockDelete()
        {
            MockGenreRepo.Setup(x => x.Delete(It.IsAny<int>()));
        }
    }
}
