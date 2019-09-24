using Community.Backend.Database.Models;
using Comunity.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comunity.Models
{
    public class Person : BaseModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirtDate { get; set; }

        public User User { get; set; }
    }
}
