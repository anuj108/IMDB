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
       public List<string> Actors { get; set; }
        public string Producer {  get; set; }

        public Movie(string title,string plot,string yearofrelease,List<string> actors,string producer)
        {
            Actors=new List<string>(actors);
            YearOfRelease = yearofrelease;
            Title=title;
            Plot=plot;
            Producer=producer;
        }
        public Movie() { }

    }

    


}
