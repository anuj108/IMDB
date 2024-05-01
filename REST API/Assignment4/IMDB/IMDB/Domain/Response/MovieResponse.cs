using System.Collections.Generic;

namespace IMDB.Domain.Response
{
    public class MovieResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int YearOfRelease { get; set; }

        public string Plot { get; set; }

        public  IEnumerable<ActorResponse> Actors { get; set; }
        public IEnumerable<GenreResponse> Genres { get; set; }
        public ProducerResponse Producer { get; set; }

        public string CoverImage { get; set; }
    }
}
