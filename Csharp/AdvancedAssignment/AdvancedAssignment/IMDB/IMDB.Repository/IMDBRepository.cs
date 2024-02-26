using IMDB.Domain;
namespace IMDB.Repository
{
    public class IMDBRepository:IIMDBRepository
    {
        private readonly List<Movie> _movie;
        private readonly List<Actor> _actor;
        public IMDBRepository()
        {
            _movie = new List<Movie>();
            _actor=new List<Actor>();
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
    }
}
