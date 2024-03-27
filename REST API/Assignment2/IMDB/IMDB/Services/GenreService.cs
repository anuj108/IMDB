using IMDB.Models;
using IMDB.Repository;
using IMDB.Repository.Interfaces;
using IMDB.Services.Interfaces;

namespace IMDB.Services
{
    public class GenreService:IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        public GenreService()
        {
            _genreRepository=new GenreRepository();
        }
        public void Create(Genre genre)
        {
            _genreRepository.Create(genre);
        }

        public IList<Genre> Get()
        {
            return _genreRepository.Get();
        }

        public Genre Get(int id)
        {
            return _genreRepository.Get(id);
        }

        public void Update(Genre genre)
        {
            _genreRepository.Update(genre);
        }
        public void Delete(int id)
        {
            _genreRepository.Delete(id);
        }
    }
}
