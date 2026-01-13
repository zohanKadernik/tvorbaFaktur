using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tvorbaFaktur.Models
{
    public class Client
    {
        public string Name { get; set; }
        public string ICO { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
