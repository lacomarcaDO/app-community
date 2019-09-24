using System;
using System.Collections.Generic;
using System.Text;
using Comunity.Models.Base;

namespace Comunity.Models
{
    public class Feedback : BaseModel
    {
        public Guid UserID { get; set; }
        public Guid SessionID { get; set; }
        public int SessionRating { get; set; }
    }
}
