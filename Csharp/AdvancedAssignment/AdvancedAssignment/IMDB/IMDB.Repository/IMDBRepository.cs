using IMDB.Domain;
namespace IMDB.Repository
{
    public class IMDBRepository:IIMDBRepository
    {
        private readonly List<Movie> _movie;
        private readonly List<Actor> _actor;
        private readonly List<Producer> _producer;
        public IMDBRepository()
        {
            _movie = new List<Movie>();
            _actor=new List<Actor>();
            _producer = new List<Producer>();
        }

        public void Add(Movie movie)
        {
            _movie.Add(movie);
        }

        public List<Movie> List()
        {
            return _movie.ToList();
        }

        public void AddActor(Actor actor)
        {
            _actor.Add(actor);
        }

        public List<Actor> ListActors()
        {
            return _actor;
        }

        public void AddProducer(Producer producer)
        {
            _producer.Add(producer);
        }

        public List<Producer> ListProducers()
        {
            return _producer;
        }

        public bool DeleteMovie(String Title)
        {
            var res=_movie.Remove(_movie.SingleOrDefault(r => r.Title==Title));
            if(res!=null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
