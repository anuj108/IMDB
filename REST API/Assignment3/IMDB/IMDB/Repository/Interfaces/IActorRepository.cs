using IMDB.Domain.Model;
using IMDB.Domain.Response;
using Microsoft.AspNetCore.Mvc;

namespace IMDB.Repository.Interfaces
{
    public interface IActorRepository
    {
        Task<IEnumerable<Actor>> Get();
        Task<Actor> Get(int id);
        Task<int> Create(Actor actor);
        Task Update(Actor actor);
        Task Delete(int id);
    }
}
