using IMDB.Models;
using IMDB.Repository.Interfaces;

namespace IMDB.Repository
{
    public class GenreRepository:IGenreRepository
    {
        private readonly List<Genre> _genres;
        public GenreRepository()
        {
            _genres = new List<Genre>();
        }
        public void Create(Genre genre)
        {
            _genres.Add(genre);
        }

        public IList<Genre> Get()
        {
            return _genres;
        }
        public Genre Get(int id)
        {
            return _genres.Where(genre => genre.Id==id).FirstOrDefault();
        }

        public void Update(Genre genre)
        {
            var genreId = _genres.FindIndex(cGenre => cGenre.Id==genre.Id);
            _genres[genreId]=genre;
        }

        public void Delete(int id)
        {
            var genreToDelete = Get(id);
            _genres.Remove(genreToDelete);
        }
    }
}
