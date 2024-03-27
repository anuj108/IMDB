namespace IMDB.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int YearOfRelease { get; set; }

        public string Plot { get; set; }

        public List<Actor> Actors {  get; set; }
        public List<Genre> Genres {  get; set; }
        public Producer Producer { get; set; }

        public string CoverImage { get; set; }
    }
}
