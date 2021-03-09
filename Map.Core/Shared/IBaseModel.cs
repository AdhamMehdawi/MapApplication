using System;

namespace Map.Core.Shared
{
    public interface IBaseEntity
    {
        int Id { get; set; }
        DateTime CreatedAt { get; set; }
        int CreatedBy { get; set; }
        bool IsDeleted { get; set; }
    }
}
