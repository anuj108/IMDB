using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMDB;
namespace IMDB.Domain
{
    public class Producer
    {
        public string Name { get; set; }
        public string DOB { get; set; }

        Producer(string name, string dob)
        {
            Name=name;
            DOB=dob;
        }
    }
}
