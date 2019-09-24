using Comunity.Models.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Community.Backend.Database.Models
{
    public class Role : IdentityRole<Guid>, IBaseModel
    {
        [Key]
        public Guid ID { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public bool Deleted { get; set; }
    }
}
