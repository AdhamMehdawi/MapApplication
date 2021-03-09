using Map.Core.Entities;
using Map.Core.Interfaces.IShapeRepositories;
using Map.Infrastructure.Data;

namespace Map.Infrastructure.Persistence.ShapeRepositories
{
    public class ShapeRepositories : Repo<Shape>, IShapeRepo
    {
        private readonly MapDbContext _db;

        public ShapeRepositories(MapDbContext context) : base(context)
        {
            _db = context;
        } 
    }
}
