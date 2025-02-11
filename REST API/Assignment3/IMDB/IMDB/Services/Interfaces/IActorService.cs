﻿using IMDB.Domain.Model;
using IMDB.Domain.Request;
using IMDB.Domain.Response;
using Microsoft.AspNetCore.Mvc;

namespace IMDB.Services.Interfaces
{
    public interface IActorService
    {
        Task<IEnumerable<ActorResponse>> Get();
        Task<ActorResponse> Get(int id);
        Task<int> Create(ActorRequest actorRequest);
        Task Update(int id, ActorRequest actorRequest);
        Task Delete(int id);
    }

}
