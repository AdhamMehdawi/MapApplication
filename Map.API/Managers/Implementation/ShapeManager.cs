using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Map.API.Helpers.SharedResponse;
using Map.API.Managers.Interfaces;
using Map.Core.Entities;
using Map.Core.Enum;
using Map.Core.Interfaces;
using Map.Core.Interfaces.IShapeRepositories;
using Map.Core.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Map.API.Managers.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public class ShapeManager : IShapeManager
    {
        private readonly IShapeRepo _shapeRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepo<Point> _pointRepo;

        #region Public Method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shapeRepo"></param>
        /// <param name="mapper"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="pointRepo"></param>
        public ShapeManager(IShapeRepo shapeRepo, IMapper mapper, IUnitOfWork unitOfWork, IRepo<Point> pointRepo)
        {
            _shapeRepo = shapeRepo;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _pointRepo = pointRepo;
        }

        /// <summary>
        /// Implement the login of add new shape 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ShapeViewModel> AddShape(ShapeViewModel model)
        {
            CheckIfValidShape(model);
            var shape = _mapper.Map<Shape>(model);
            var newShape = await _shapeRepo.AddAsync(shape);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<ShapeViewModel>(newShape);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ShapeViewModel> GetShapeById(int id)
        {
            var shape = await GetShapeModelViaId(id);
            return _mapper.Map<ShapeViewModel>(shape);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageParams"></param>
        /// <returns></returns>
        public async Task<PageViewModel<ShapeViewModel>> GetShapeList(PageParamsList pageParams)
        {
            var shapeList = await _shapeRepo.GetPage(pageParams.CurrentPage, pageParams.PageSize, null,
                null, c => c.Include(t => t.ShapePoints));
            return _mapper.Map<PageViewModel<ShapeViewModel>>(shapeList);
        }

        /// <summary>
        /// implement the update logic 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ShapeViewModel> UpdateShape(int id, ShapeViewModel model)
        {
            CheckIfValidShape(model);
            var shape = await GetShapeModelViaId(id);
            //Delete Shape Points
            await _pointRepo.DeleteAll(shape.ShapePoints.ToList());
            var newShapeObj = _mapper.Map(model, shape);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<ShapeViewModel>(newShapeObj);
        }

        /// <summary>
        /// implement delete login 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteShape(int id)
        {
            var entity = await GetShapeModelViaId(id);
            await _pointRepo.DeleteAll(entity.ShapePoints.ToList());
            await _shapeRepo.Delete(entity);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        #endregion

        #region Private Method

        private void CheckIfValidShape(ShapeViewModel model)
        {
            //check if the shape of circle type, then the radius is required 
            if ((ShapeType)model.ShapeType == ShapeType.Circle)
            {
                if (model.Radius == null || model.Radius < 0.1)
                    throw new ShredValidationException(
                        "Invalid radius value, the posted shape of circle type  so the Radius is required value");
                if (model.ShapePoints.Count > 1)
                    throw new ShredValidationException(
                        "Invalid circle center, the point list contained more than one point");
            }
        }

        private async Task<Shape> GetShapeModelViaId(int id)
        {
            if (id <= 0)
                throw new ShredValidationException(
                    "The id value is incorrect please make sure to send value grater then 0");
            var shape = await _shapeRepo.GetAsync(c => c.Id == id, c => c.Include(t => t.ShapePoints));
            if (shape == null)
                throw new ShredNotFoundException($"Shape not found. the id  {id} incorrect value !");
            return shape;
        }

        #endregion
    }
}
