using IMDB.Domain.Model;
using IMDB.Domain.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB.Repository.Interfaces
{
    public interface IActorRepository
    {
        Task<IEnumerable<Actor>> Get();
        Task<Actor> GetById(int id);
        Task<int> Create(Actor actor);
        Task Update(int Id,Actor actor);
        Task Delete(int id);
        Task<IEnumerable<Actor>> GetActorsForMovie(int id);
    }
}
