using IMDB.Domain;
using IMDB.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Services
{
    public interface IIMDBService  //INTERFACE FOR IMDBService Class
    {
       

        public void AddMovie(string title,string yearOfRelease, string plot, string actors, string producer);


        public List<Movie> ListMovie();

        public void AddActor(string name, DateTime dob);
        public List<Actor> ListActor();

        public void AddProducer(string name,DateTime dob);

        public List<Producer> ListProducer();
       
        public bool DeleteMovie(string title);
    }
}
