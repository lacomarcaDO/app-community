using Comunity.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comunity.Models
{
    public class Notification:BaseModel
    {
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
