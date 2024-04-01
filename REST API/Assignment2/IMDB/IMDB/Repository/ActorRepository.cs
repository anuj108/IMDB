using IMDB.Domain.Model;
using IMDB.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IMDB.Repository
{
    public class ActorRepository:IActorRepository
    {
        private readonly List<Actor> _actors;
        public ActorRepository()
        { 
            _actors = new List<Actor>();
        }
        public IList<Actor> Get()
        {
            return _actors;
        }
        public Actor Get(int id)
        {
            return _actors.Where(actor => actor.Id==id).FirstOrDefault();
        }
        public Actor Create(Actor actor)
        {
            _actors.Add(actor);
             return actor;
        }
        public void Update(Actor actor)
        {
            var actorId = _actors.FindIndex(cActor => cActor.Id==actor.Id);
            _actors[actorId]=actor;
        }
        public void Delete(int id)
        {
            var actorToDelete = Get(id);
            _actors.Remove(actorToDelete);
        }
    }
}
