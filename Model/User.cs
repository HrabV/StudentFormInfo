using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studentforms.Model
{
    public class User
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronomic { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime Bday { get; set; }
        public string Gender { get; set; }

    }
}
