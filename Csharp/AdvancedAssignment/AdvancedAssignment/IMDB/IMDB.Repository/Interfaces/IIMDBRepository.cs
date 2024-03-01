using IMDB.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Repository
{
    public interface IIMDBRepository
    {
        public void Add(Movie movie);
       

        public List<Movie> List();

        public void AddActor(Actor actor);

        public List <Actor> ListActors();


        public void AddProducer(Producer producer);

        public List<Producer> ListProducers();
        public bool DeleteMovie(String Title);
    }
}
