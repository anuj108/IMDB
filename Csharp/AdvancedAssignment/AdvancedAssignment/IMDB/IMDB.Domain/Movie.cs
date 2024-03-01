using System;
using System.Collections.Generic;
using System.Text;
namespace IMDB.Domain
{
    public class Movie
    {
        public string Title { get; set; }
        public string YearOfRelease {  get; set; }
        public string Plot{ get; set; }
       public List<Actor> Actors { get; set; }
        public Producer Producer {  get; set; }

        public Movie(string title, string yearofrelease,string plot,List<Actor> actors,Producer producer)
        {
            Actors=new List<Actor>(actors);
            YearOfRelease = yearofrelease;
            Title=title;
            Plot=plot;
            Producer=producer;
        }
        public Movie() { }

    }

    


}
