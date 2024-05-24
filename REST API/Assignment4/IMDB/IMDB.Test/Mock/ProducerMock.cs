using IMDB.Domain.Model;
using IMDB.Repository.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;


namespace IMDB.Test.Mock
{
    public class ProducerMock
    {
        public static readonly Mock<IProducerRepository> MockProducerRepo = new Mock<IProducerRepository>();
        public static List<Producer> ListOfProducers = new List<Producer>()
        {
            new Producer
            {
                Id=1,
                Name="ABC",
                Bio="some info",
                DOB=new DateTime(1999,08,08),
                Gender="Male"
            },
            new Producer
            {
                Id=2,
                Name="ABC",
                Bio="some info",
                DOB=new DateTime(1999,08,08),
                Gender="Male"
            }

        };

        public static void MockGetAll()    
        {
            MockProducerRepo.Setup(x=>x.Get()).ReturnsAsync(ListOfProducers);
        }

        public static void MockGetById()
        {
            MockProducerRepo.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((int id)=>ListOfProducers.FirstOrDefault(y=>y.Id==id));
        }

        public static void MockCreate()
        {
            MockProducerRepo.Setup(x=>x.Create(It.IsAny<Producer>())).ReturnsAsync(ListOfProducers.Max(x => x.Id) + 1);
        }

        public static void MockUpdate()
        {
            MockProducerRepo.Setup(x => x.Update(It.IsAny<Producer>()));
        }

        public static void MockDelete()
        {
            MockProducerRepo.Setup(x => x.Delete(It.IsAny<int>()));
        }
    }
}
