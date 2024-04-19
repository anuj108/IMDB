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
    public  class ActorMock
    {
         public static readonly Mock<IActorRepository> MockActorRepo = new Mock<IActorRepository>();
        public static List<Actor> ListOfActors = new List<Actor>()
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
        public static void MockCreate()
        {
            MockActorRepo.Setup(x => x.Create(It.IsAny<Actor>())).ReturnsAsync(ListOfActors.Max(x => x.Id) + 1);
        }
        public static void MockGetAll()
        {
            MockActorRepo.Setup(x => x.Get()).ReturnsAsync(ListOfActors);
        }

        public static void MockGetById()
        {
            MockActorRepo.Setup(x=>x.Get(It.IsAny<int>())).ReturnsAsync((int id)=>ListOfActors.FirstOrDefault(x=>x.Id==id));
        }

        public static void MockUpdate()
        {
            MockActorRepo.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Actor>()));
        }

        public static void MockDelete()
        {
            MockActorRepo.Setup(x => x.Delete(It.IsAny<int>()));
        }

    }
}
