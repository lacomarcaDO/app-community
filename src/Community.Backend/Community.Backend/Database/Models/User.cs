using Community.Backend.Database.Models.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community.Backend.Database.Models
{
    public class User:IdentityUser<Guid>, IBaseModel
    {
        public Guid PersonID { get; set; }
        public Person Person { get; set; }
        public Guid ID { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public bool Deleted { get; set; }
    }
}
