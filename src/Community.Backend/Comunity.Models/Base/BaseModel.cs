using System;
using System.ComponentModel.DataAnnotations;

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
