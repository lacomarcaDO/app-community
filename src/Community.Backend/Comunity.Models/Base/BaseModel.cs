using System;
using System.Collections.Generic;
using System.Text;

namespace Comunity.Models.Base
{
    public interface IBaseModel
    {
        [Key]
        public Guid ID { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public bool Deleted { get; set; }
    }
    public abstract class BaseModel : IBaseModel
    {
        [Key]
        public Guid ID { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public bool Deleted { get; set; }
    }
}
