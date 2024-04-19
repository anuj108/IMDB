namespace IMDB.Domain.Response
{
    public class MovieResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int YearOfRelease { get; set; }

        public string Plot { get; set; }

        public string Actors { get; set; }
        public string Genres { get; set; }
        public string Producer { get; set; }

        public string CoverImage { get; set; }
    }
}
