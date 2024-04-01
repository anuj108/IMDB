using IMDB.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace IMDB.Repository.Interfaces
{
    public interface IActorRepository
    {
        IList<Actor> Get();
        Actor Get(int id);
        Actor Create(Actor actor);
        void Update(Actor actor);
        void Delete(int id);
    }
}
