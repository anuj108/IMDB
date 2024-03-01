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
    public class InvalidTitleException : Exception
    {
        public InvalidTitleException(string message) : base(message) { }
    }

    // Custom exception for invalid year of release
    public class InvalidYearOfReleaseException : Exception
    {
        public InvalidYearOfReleaseException(string message) : base(message) { }
    }

    // Custom exception for invalid list of actors
    public class InvalidActorsListException : Exception
    {
        public InvalidActorsListException(string message) : base(message) { }
    }

    // Custom exception for invalid producer
    public class InvalidProducerException : Exception
    {
        public InvalidProducerException(string message) : base(message) { }
    }

    // Custom exception for invalid plot
    public class InvalidPlotException : Exception
    {
        public InvalidPlotException(string message) : base(message) { }
    }

    public class InvalidDeleteException : Exception
    {
        public InvalidDeleteException(string message) : base(message) { }
    }

    public class InvalidNameException : Exception
    {
        public InvalidNameException(string message) : base(message) { }
    }

    public class IMDBService:IIMDBService
    {
        private readonly IIMDBRepository _imdbrepository;
        public IMDBService()
        {
            _imdbrepository = new IMDBRepository();
        }

        public void AddMovie(string title, string yearOfRelease, string plot,string actors, string producer)
        {

            //validation

            int result;

            if(string.IsNullOrEmpty(title))
            {
                throw new InvalidTitleException("TITLE CANNOT BE EMPTY!!! PLEASE TRY AGAIN  --------------------------------------");
            }
           

            if (string.IsNullOrEmpty(yearOfRelease)||Convert.ToInt32(yearOfRelease) < 1800 || Convert.ToInt32(yearOfRelease) > DateTime.Now.Year||string.IsNullOrEmpty(yearOfRelease))
                throw new InvalidYearOfReleaseException("INVALID YEAR OF RELEASE  PLEASE TRY AGAIN \n --------------------------------------");

            if (string.IsNullOrEmpty(actors))
                throw new InvalidActorsListException("LIST OF ACTORS CANNOT BE EMPTY  PLEASE TRY AGAIN \n --------------------------------------");

            if (string.IsNullOrEmpty(producer))
                throw new InvalidProducerException("PRODUCER CAN NOT BE EMPTY  PLEASE TRY AGAIN \n --------------------------------------");

            if (string.IsNullOrEmpty(plot))
                throw new InvalidPlotException("PLOT CANNOT BE EMPTY  PLEASE TRY AGAIN \n --------------------------------------");

           
            var flag = false;
            string[] choiceindex = actors.Split(',');
              
                for (var j = 0; j<choiceindex.Length; j++)
                {
                    if (int.TryParse(choiceindex[j], out result))
                    {
                        if (result-1>=ListActor().Count()||result<0)
                        {
                            throw new InvalidActorsListException("INVALID INDEX!!  PLEASE TRY AGAIN \n --------------------------------------");
                            
                        }
                    }
                    else
                    {
                        throw new InvalidActorsListException("WRONG FORMAT!!  PLEASE TRY AGAIN \n --------------------------------------");
                    }


                }


            if (int.TryParse(producer, out  result))
            {
                if (result-1>=ListProducer().Count()||result<0)
                {
                    throw new InvalidProducerException("INVALID INDEX!!  PLEASE TRY AGAIN \n --------------------------------------");
                }

            }
            else
            {
                throw new InvalidProducerException("WRONG FORMAT!!  PLEASE TRY AGAIN \n --------------------------------------");
            }
               
           

            List<Actor> selectedactors = choiceindex.Select(index => ListActor()[Convert.ToInt32(index)-1]).ToList();
            Producer selectedproducer = ListProducer()[Convert.ToInt32(producer)-1];
            Movie movie = new Movie(title,yearOfRelease, plot, selectedactors,selectedproducer);
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
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidNameException("NAME CANNOT BE EMPTY");
            }

            Actor actor = new Actor(name, dateofbirth);
            _imdbrepository.AddActor(actor);
        }

        public List<Actor> ListActor()
        {
            return _imdbrepository.ListActors();
        }

        public void AddProducer(string name, DateTime dateofbirth)
        {
            if (dateofbirth > DateTime.Now)
            {
                throw new InvalidDateOfBirthException("Date of birth cannot be in the future.");
            }
            if (dateofbirth == DateTime.MinValue || dateofbirth == DateTime.MaxValue)
            {
                throw new InvalidDateOfBirthException("Invalid date of birth.");
            }
            if(string.IsNullOrEmpty(name))
            {
                throw new InvalidNameException("NAME CANNOT BE EMPTY");
            }
            Producer producer = new Producer(name, dateofbirth);
            _imdbrepository.AddProducer(producer);
        }

        public List<Producer> ListProducer()
        {
            return _imdbrepository.ListProducers();
        }

        public bool DeleteMovie(string index)
        {
            //validation
            if (string.IsNullOrEmpty(index))
            {
                throw new InvalidDeleteException("INDEX CANNOT BE EMPTY!!! PLEASE TRY AGAIN \n --------------------------------------");
            }
            if (int.TryParse(index, out int result))
            {
                if (result-1>=ListMovie().Count()||result<0)
                {
                    throw new InvalidDeleteException("INVALID INDEX!!  PLEASE TRY AGAIN \n --------------------------------------");
                }

            }
            else
            {
                throw new InvalidDeleteException("WRONG FORMAT!!  PLEASE TRY AGAIN \n --------------------------------------");
            }
            return _imdbrepository.DeleteMovie(ListMovie()[Convert.ToInt32(index)-1].Title);
        }
    }
}
