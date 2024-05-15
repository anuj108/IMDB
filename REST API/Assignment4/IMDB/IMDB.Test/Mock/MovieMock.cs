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
    public class MovieMock
    {
        public static readonly Mock<IMovieRepository> MockMovieRepo=new Mock<IMovieRepository>();
        public static readonly List<Movie> ListOfMovies = new List<Movie>()
        {
        new Movie{
            Id = 1,
            Name = "Foo",
            YearOfRelease = 2000,
            Plot="something",
            CoverImage="1.jpg",
            Producer=1
        }
        };



        public static void MockGetAll()
        {
            MockMovieRepo.Setup(x=>x.Get()).ReturnsAsync(ListOfMovies);
        }
        public static void MockCreate()
        {
            MockMovieRepo.Setup(x => x.Create(It.IsAny<Movie>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(ListOfMovies.Max(x=>x.Id)+1);
        }

        public static void MockGetById()
        {
            MockMovieRepo.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((int id)=>ListOfMovies.FirstOrDefault(y=>y.Id==id));
        }

        public static void MockDelete()
        {
            MockMovieRepo.Setup(x => x.Delete(It.IsAny<int>()));
        }

        public static void MockUpdate()
        {
            MockMovieRepo.Setup(x => x.Update(It.IsAny<Movie>(), It.IsAny<string>(), It.IsAny<string>()));
        }

        public static void MockGetMoviesByYear()
        {
            MockMovieRepo.Setup(x => x.GetByYear(It.IsAny<int>())).ReturnsAsync((int year) => ListOfMovies.Where(x => x.YearOfRelease==year));
        }

    }
}
