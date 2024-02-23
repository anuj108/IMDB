using IMDB.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMDB.Repository;
using System.Diagnostics;
using System.Xml.Linq;
using System.Collections;


namespace IMDB.Services
{
    public class IMDBService:IIMDBService
    {
        private readonly IIMDBRepository _imdbrepository;
        public IMDBService()
        {
            _imdbrepository = new IMDBRepository();
        }

        public void AddMovie(string title,string plot, string yearofrelease, List<string> actors, string producer)
        {//validation

            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(plot) || string.IsNullOrEmpty(producer) || string.IsNullOrEmpty(yearofrelease)||((actors!= null) && (!actors.Any())))
            {
                
                throw new ArgumentException("Invalid arguments");
            }

            Movie movie = new Movie(title,plot,Convert.ToInt32(yearofrelease),actors,producer);
            _imdbrepository.Add(movie);
        }

        public List<Movie> ListMovie()
        {
            return _imdbrepository.List();
        }
    }
}
