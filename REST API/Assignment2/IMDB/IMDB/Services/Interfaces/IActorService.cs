using IMDB.Domain.Model;
using IMDB.Domain.Request;
using IMDB.Domain.Response;
using Microsoft.AspNetCore.Mvc;

namespace IMDB.Services.Interfaces
{
    public interface IActorService
    {
        List<ActorResponse> Get();
        ActorResponse Get(int id);
        Actor Create(ActorRequest actorRequest);
        void Update(int id, ActorRequest actorRequest);
        void Delete(int id);
    }

}
