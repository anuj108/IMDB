using IMDB.Domain.Model;
using IMDB.Domain.Request;
using IMDB.Domain.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB.Services.Interfaces
{
    public interface IActorService
    {
        Task<IEnumerable<ActorResponse>> Get();
        Task<ActorResponse> GetById(int id);
        Task<int> Create(ActorRequest actorRequest);
        Task Update(int id, ActorRequest actorRequest);
        Task Delete(int id);
        Task<IEnumerable<ActorResponse>> GetActorsForMovie(int id);
    }

}
