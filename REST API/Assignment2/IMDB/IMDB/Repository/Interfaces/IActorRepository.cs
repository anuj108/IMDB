using IMDB.Models;

namespace IMDB.Repository.Interfaces
{
    public interface IActorRepository
    {
        IList<Actor> Get();
        Actor Get(int id);
        void Create(Actor actor);
        void Update(Actor actor);
        void Delete(int id);
    }
}
