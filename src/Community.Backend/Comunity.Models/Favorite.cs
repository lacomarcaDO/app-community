using Comunity.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comunity.Models
{
    public class Favorite : BaseModel
    {
        public Guid UserID { get; set; }
        public Guid SessionID { get; set; }
    }
}
