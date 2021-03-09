using System.Threading.Tasks;
using Map.Core.ViewModels;

namespace Map.API.Managers.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IShapeManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ShapeViewModel> AddShape(ShapeViewModel model);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ShapeViewModel> GetShapeById(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageParams"></param>
        /// <returns></returns>
        Task<PageViewModel<ShapeViewModel>> GetShapeList(PageParamsList pageParams);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ShapeViewModel> UpdateShape(int id, ShapeViewModel model);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteShape(int id); 
     }
}