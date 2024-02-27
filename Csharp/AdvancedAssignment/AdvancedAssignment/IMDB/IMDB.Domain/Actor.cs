using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Domain
{
    public class Actor
    {
        public string Name { get; set; }
        public DateTime DOB { get; set; }

        public Actor(string name,DateTime dob)
        {
            Name=name;
            DOB=dob;
        }
        public Actor() { }
    }
}
