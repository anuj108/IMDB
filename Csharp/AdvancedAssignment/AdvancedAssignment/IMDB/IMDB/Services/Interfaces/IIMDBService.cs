using IMDB.Domain;
using IMDB.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Services
{
    public interface IIMDBService
    {
       

        public void AddMovie(string title, string plot, string yearofrelease, List<string> actors, string producer);


        public List<Movie> ListMovie();
       
    }
}
