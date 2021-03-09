using AutoMapper;
using Map.Core.Entities;
using Map.Core.ViewModels;

namespace Map.API.Helpers.Mapper
{
    /// <summary>
    /// 
    /// </summary>
    public class MapMapperProfile : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public MapMapperProfile()
        {
            CreateMap<ShapeViewModel, Shape>().ReverseMap();
            CreateMap<PointViewModel, Point>().ReverseMap();
            CreateMap<PageViewModel<ShapeViewModel>, PageViewModel<Shape>>().ReverseMap();
        }
    }
}
