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
    public class ActorMock
    {

        public static readonly Mock<IActorRepository> MockActorRepo = new Mock<IActorRepository>();
        public static List<Actor> ActorList = new List<Actor>()
        {
            new Actor
            {
                Id = 1,
                Name = "Actor1",
                DOB = new DateTime(1999,07,08),
                Bio = "Bio1",
                Gender = "Male"
            },
            new Actor
            {
                Id = 2,
                Name = "Actor2",
                DOB = new DateTime(1999,07,08),
                Bio = "Bio2",
                Gender = "Male"
            },
            new Actor
            {
                Id = 3,
                Name = "Actor3",
                DOB = new DateTime(1999,07,08),
                Bio = "Bio3",
                Gender = "male"
            }
        };

        public static List<Actors_Movies> ActorsMovies = new List<Actors_Movies>()
        {
            new Actors_Movies
            {
                actorId=1,
                movieId=1
            }
        };
        public static void MockCreate()
        {
            MockActorRepo.Setup(x => x.Create(It.IsAny<Actor>())).ReturnsAsync(ActorList.Max(x => x.Id) + 1);
        }
        public static void MockGetAll()
        {
            MockActorRepo.Setup(x => x.Get()).ReturnsAsync(ActorList);
        }

        public static void MockGetById()
        {
            MockActorRepo.Setup(x=>x.GetById(It.IsAny<int>())).ReturnsAsync((int id)=> ActorList.FirstOrDefault(x=>x.Id==id));
        }

        public static void MockUpdate()
        {
            MockActorRepo.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Actor>()));
        }

        public static void MockDelete()
        {
            MockActorRepo.Setup(x => x.Delete(It.IsAny<int>()));
        }

        public static void MockGetActorsForMovie()
        {
            MockActorRepo.Setup(x=>x.GetActorsForMovie(It.IsAny<int>())).ReturnsAsync((int id)=>
            {
                var actorIds = ActorsMovies.Where(y=>y.movieId==id).Select(y=>y.actorId);
                return ActorList.Where(y => actorIds.Contains(y.Id));
            });
        }

    }
}
