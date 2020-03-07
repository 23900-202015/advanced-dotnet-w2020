using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Week8AspNetCore.Models
{
    public class PersonViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public PersonViewModel()
        {

        }
        public PersonViewModel(Person person)
        {
            this.DateOfBirth = person.DateOfBirth;
            this.FirstName = person.FirstName;
            this.LastName = person.LastName;
        }
    }
}
