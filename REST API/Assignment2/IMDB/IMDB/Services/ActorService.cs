using IMDB.Models;
using IMDB.Repository;
using IMDB.Repository.Interfaces;
using IMDB.Services.Interfaces;

namespace IMDB.Services
{
    public class ActorService : IActorService
    {

        private readonly IActorRepository _actorRepository;
        public ActorService()
        {

            _actorRepository = new ActorRepository();
        }
        public void Create(Actor actor)
        {
            _actorRepository.Create(actor);
        }

        public IList<Actor> Get()
        {
            return _actorRepository.Get();
        }

        public Actor Get(int id)
        {
           return _actorRepository.Get(id);
        }

        public void Update(Actor actor)
        {
            _actorRepository.Update(actor);
        }

        public void Delete(int id)
        {
            _actorRepository.Delete(id);
        }
    }
}
