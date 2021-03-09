using System;

namespace Map.Core.Shared
{
    public class BaseEntity : IBaseEntity
    {
        public virtual int Id { get; set; }
        public DateTime CreatedAt { get; set; }
         public int CreatedBy { get; set; }
         public bool IsDeleted { get; set; }
    }
     
}
