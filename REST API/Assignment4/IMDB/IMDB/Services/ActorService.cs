using IMDB.CustomExceptions;
using IMDB.Domain.Model;
using IMDB.Domain.Request;
using IMDB.Domain.Response;
using IMDB.Repository.Interfaces;
using IMDB.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB.Services
{
    public class ActorService : IActorService
    {

        private readonly IActorRepository _actorRepository;

        public ActorService(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }

        //TO GET ALL THE ACTORS
        public async Task<IEnumerable<ActorResponse>> Get()
        {

            var actorData = await _actorRepository.Get();
            if (!actorData.Any()) throw new NotFoundException("EMPTY ACTOR LIST");
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
        public async Task<ActorResponse> GetById(int id)
        {
            var actorData = await _actorRepository.GetById(id);
            if (actorData == null) throw new NotFoundException("NO ACTOR FOUND");
            return new ActorResponse()
            {
                Id = actorData.Id,
                Name = actorData.Name,
                Bio = actorData.Bio,
                DOB = actorData.DOB,
                Gender = actorData.Gender
            };
        }

        //TO CREATE AN ACTOR
        public async Task<int> Create(ActorRequest actorRequest)
        {

            if (string.IsNullOrWhiteSpace(actorRequest.Name)) throw new BadRequestException("INVALID ACTORNAME");
            if (string.IsNullOrWhiteSpace(actorRequest.Bio)) throw new BadRequestException("INVALID ACTOR BIO");
            if (actorRequest.DOB.Year < 1800 || actorRequest.DOB.Year > DateTime.Now.Year) throw new BadRequestException("INVALID DATE OF BIRTH");
            if (!actorRequest.Gender.Equals("Male") && !actorRequest.Gender.Equals("Female")) throw new BadRequestException("INVALID GENDER");

            return await _actorRepository.Create(
                new Actor {
                    Name = actorRequest.Name,
                    Bio = actorRequest.Bio,
                    DOB = actorRequest.DOB,
                    Gender = actorRequest.Gender
                });
        }



        //TO UPDATE AN ACTOR
        public async Task Update(int id, ActorRequest actorRequest)
        {
            if (await _actorRepository.GetById(id)==null) throw new NotFoundException("NO ACTOR FOUND");

            if (string.IsNullOrWhiteSpace(actorRequest.Name)) throw new BadRequestException("INVALID ACTOR NAME");
            if (string.IsNullOrWhiteSpace(actorRequest.Bio)) throw new BadRequestException("INVALID ACTOR BIO");
            if (actorRequest.DOB.Year < 1800 || actorRequest.DOB.Year > DateTime.Now.Year) throw new BadRequestException("INVALID DATE OF BIRTH");
            if (!actorRequest.Gender.Equals("Male") && !actorRequest.Gender.Equals("Female")) throw new BadRequestException("INVALID GENDER");
            await _actorRepository.Update(id, new Actor
            {
                Id = id,
                Name = actorRequest.Name,
                Bio = actorRequest.Bio,
                DOB = actorRequest.DOB,
                Gender = actorRequest.Gender
            });
        }

        //TO DELETE AN ACTOR
        public async Task Delete(int id)
        {
            if (await _actorRepository.GetById(id)==null) throw new NotFoundException("NO ACTOR FOUND");
            await _actorRepository.Delete(id);
        }

        //TO GET STRING OF IDS OF ACTORS USING ACTORS_MOVIES TABLE
        public async Task<IEnumerable<ActorResponse>> GetActorsForMovie(int id)
        {
            var actorData= await _actorRepository.GetActorsForMovie(id);
            return actorData.Select(x => new ActorResponse(){
                Id = x.Id,
                Name = x.Name,
                Bio = x.Bio,
                DOB = x.DOB,
                Gender = x.Gender
            }).ToList();

        }
    }
}
