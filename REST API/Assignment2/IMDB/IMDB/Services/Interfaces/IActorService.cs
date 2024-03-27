using IMDB.Models;

namespace IMDB.Services.Interfaces
{
    public interface IActorService
    {
        IList<Actor> Get();
        Actor Get(int id);
        void Create(Actor actor);
        void Update(Actor actor);
        void Delete(int id);
    }

}
