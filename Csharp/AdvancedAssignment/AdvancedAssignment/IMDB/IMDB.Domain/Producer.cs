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
        public DateTime DOB { get; set; }

        public Producer(string name, DateTime dob)
        {
            Name=name;
            DOB=dob;
        }

        public Producer() { }
    }
}
