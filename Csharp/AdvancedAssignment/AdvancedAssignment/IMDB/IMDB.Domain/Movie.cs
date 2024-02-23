using System;
using System.Collections.Generic;
using System.Text;
namespace IMDB.Domain
{
    public class Movie
    {
        public string Title { get; set; }
        public int YearOfRelease {  get; set; }
        public string Plot{ get; set; }
       public List<string> _actors=new List<String>();
        public string Producer {  get; set; }

        public Movie(string title,string plot,int yearofrelease,List<string> actors,string producer)
        {
            _actors.AddRange(actors);
            YearOfRelease = yearofrelease;
            Title=title;
            Plot=plot;
            Producer=producer;
        }
        public Movie() { }

    }

    


}
