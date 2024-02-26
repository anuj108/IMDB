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
    public class InvalidDateOfBirthException : Exception
    {
        public InvalidDateOfBirthException() { }

        public InvalidDateOfBirthException(string message) : base(message) { }

        
    }

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

            Movie movie = new Movie(title,plot,yearofrelease,actors,producer);
            _imdbrepository.Add(movie);
        }

        public List<Movie> ListMovie()
        {
            return _imdbrepository.List();
        }

        public void AddActor(string name,DateTime dateofbirth)
        {
            if (dateofbirth > DateTime.Now)
            {
                throw new InvalidDateOfBirthException("Date of birth cannot be in the future.");
            }
            if (dateofbirth == DateTime.MinValue || dateofbirth == DateTime.MaxValue)
            {
                throw new InvalidDateOfBirthException("Invalid date of birth.");
            }

            Actor actor = new Actor(name, dateofbirth);
            _imdbrepository.AddActor(actor);
        }

        public List<Actor> ListActor()
        {
            return _imdbrepository.ListActors();
        }
    }
}
