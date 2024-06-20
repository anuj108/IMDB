using IMDB.CustomExceptions;
using IMDB.Domain.Model;
using IMDB.Domain.Request;
using IMDB.Domain.Response;
using IMDB.Repository;
using IMDB.Repository.Interfaces;
using IMDB.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Security.Cryptography;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IMDB.Services
{
    public class ActorService : IActorService
    {

        private readonly IActorRepository _actorRepository;
        private int _id = 0;
        public ActorService(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }

        //TO GET ALL THE ACTORS
        public List<ActorResponse> Get()
        {
            
            var actorData = _actorRepository.Get();
            if (actorData==null) throw new BadRequestException("Empty Actor List returned");
            return actorData.Select(x => new ActorResponse()
            {
                Id = x.Id,
                Name = x.Name,
                Bio = x.Bio,
                DOB = x.DOB,
                Gender = x.Gender
            }).ToList();
        }

        //TO GET THE ACTOR BY ID
        public ActorResponse Get(int id)
        {
            var actor = _actorRepository.Get(id);
            if (actor==null) throw new BadRequestException("Actor not Found");

            return new ActorResponse()
            {
                Id = actor.Id,
                Name = actor.Name,
                Bio = actor.Bio,
                DOB = actor.DOB,
                Gender = actor.Gender
            };
        }


        public Actor Create(ActorRequest actorRequest)
        {
            
            if (string.IsNullOrWhiteSpace(actorRequest.Name)) throw new BadRequestException("Invalid Name");
            if (string.IsNullOrWhiteSpace(actorRequest.Bio)) throw new BadRequestException("Invalid Bio Data");
            if (actorRequest.DOB.Year < 1800 || actorRequest.DOB.Year > DateTime.Now.Year) throw new BadRequestException("Invalid Date of Birth");
            if (!actorRequest.Gender.Equals("Male") && !actorRequest.Gender.Equals("Female")) throw new BadRequestException("Invalid Gender");
            _id++;
            return _actorRepository.Create(
                new Actor { Id=_id,
                Name = actorRequest.Name,
                Bio = actorRequest.Bio,
                DOB = actorRequest.DOB,
                Gender = actorRequest.Gender
                });
        }

        

       
        public void Update(int id,ActorRequest actorRequest)
        {
            if (id>_actorRepository.Get().Last().Id || id<=0) throw new BadRequestException("Invalid Id");
            if (string.IsNullOrWhiteSpace(actorRequest.Name)) throw new BadRequestException("Invalid Name");
            if (string.IsNullOrWhiteSpace(actorRequest.Bio)) throw new BadRequestException("Invalid Bio Data");
            if (actorRequest.DOB.Year < 1800 || actorRequest.DOB.Year > DateTime.Now.Year) throw new BadRequestException("Invalid Date of Birth");
            if (!actorRequest.Gender.Equals("Male") && !actorRequest.Gender.Equals("Female")) throw new BadRequestException("Invalid Gender");
            _actorRepository.Update(new Actor
            {
                Id = id,
                Name = actorRequest.Name,
                Bio = actorRequest.Bio,
                DOB = actorRequest.DOB,
                Gender = actorRequest.Gender
            });
        }

        public void Delete(int id)
        {
            var actorData = _actorRepository.Get(id);
            if (actorData==null) throw new BadRequestException("Invalid Id");
            _actorRepository.Delete(id);
        }

    }
}
