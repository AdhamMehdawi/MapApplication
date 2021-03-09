using System.Threading.Tasks;
using Map.API.Helpers.SharedResponse;
using Map.API.Managers.Interfaces;
using Map.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Map.API.Controllers
{
    /// <summary>
    /// Shape API To create, list, delete and get shape by id 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ShapeController : ControllerBase
    {
        private readonly IShapeManager _shapeManager;
        /// <summary>
        /// The shape Constructor 
        /// </summary>
        public ShapeController(IShapeManager shapeManager)
        {
            _shapeManager = shapeManager;
        }
        /// <summary>
        /// Get list of shapes 
        /// </summary>
        /// <returns></returns>
        [HttpGet("Get")]
        [ProducesResponseType(typeof(SharedResponse<PageViewModel<ShapeViewModel>>), 200)]
        public async Task<IActionResult> Get([FromHeader] PageParamsList page)
        {
            var result = await _shapeManager.GetShapeList(page);
            return new SharedResponseResult<PageViewModel<ShapeViewModel>>(result);
        }

        /// <summary>
        /// Get shape by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:min(1)}")]
        [ProducesResponseType(typeof(SharedResponse<ShapeViewModel>), 200)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _shapeManager.GetShapeById(id);
            return new SharedResponseResult<ShapeViewModel>(result);
        }

        /// <summary>
        /// To Add new Shape 
        /// </summary>
        /// <param name="model">The shape model</param>
        /// <response code="200">Shape created</response>
        /// <response code="400">Shape has missing/invalid values</response>
        /// <response code="500">Oops! Can't create your product right now</response>
        /// <returns></returns>
        [HttpPost("Post")]
        [ProducesResponseType(typeof(SharedResponse<ShapeViewModel>), 200)]
        public async Task<IActionResult> Post(ShapeViewModel model)
        {
            var result = await _shapeManager.AddShape(model);
            return new SharedResponseResult<ShapeViewModel>(result, "The shape has been created successfully");
        }

        /// <summary>
        /// Update the shape 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:min(1)}")]
        [ProducesResponseType(typeof(SharedResponse<ShapeViewModel>), 200)]
        public async Task<IActionResult> UpdateShape(int id, [FromBody] ShapeViewModel model)
        {
            var result = await _shapeManager.UpdateShape(id, model);
            return new SharedResponseResult<ShapeViewModel>(result, "The shape has been updated successfully");
        }

        /// <summary>
        /// Implement delete logic 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:min(1)}")]
        [ProducesResponseType(typeof(SharedResponse<bool>), 200)]
        public async Task<IActionResult> DeleteShape(int id)
        {
            var result = await _shapeManager.DeleteShape(id);
            return new SharedResponseResult<bool>(result, "The shape has been deleted successfully");
        }


    }
}
