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
        public string DOB { get; set; }

        Actor(string name,string dob)
        {
            Name=name;
            DOB=dob;
        }
    }
}
