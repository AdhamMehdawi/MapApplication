using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Map.Core.Enum;

namespace Map.Core.ViewModels
{
    /// <summary>
    /// The Shape Object Model
    /// </summary>
    public class ShapeViewModel
    {
        /// <summary>
        /// The Shape Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The Shape Title
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The Shape title is required"),
         StringLength(200, MinimumLength = 3, ErrorMessage = "The Title must be less than 200 chars and more than 2")]
        public string Title { get; set; }
        /// <summary>
        /// The color will fill into shape
        /// </summary>
        public string FillColorCode { get; set; }
        /// <summary>
        /// The filled color opacity
        /// </summary>
        [Range(0.0, 1.0, ErrorMessage = "The Fill Opacity must be between 0.0 and 1.0")]
        public double FillOpacity { get; set; }
        /// <summary>
        /// The shape  stroke weight and the value must
        /// be between 0.0 and 1.0 and the default 
        /// </summary>
        [Range(0.0, 1.0, ErrorMessage = "The Stroke Weight must be between 0.0 and 1.0")]
        public int StrokeWeight { get; set; }
        /// <summary>
        /// The shape stroke color 
        /// </summary>
        public string StrokeColorCode { get; set; }
        /// <summary>
        /// The Geodesic, the distance between tow point is strait or follow the geometry concepts   
        /// </summary>
        public bool Geodesic { get; set; }
        /// <summary>
        /// The zoom level value  
        /// </summary>
        [Range(0.0, 99, ErrorMessage = "The Zoom Level must be between 0 and 99")]
        public int ZoomRatio { get; set; }
        /// <summary>
        /// if the shape is circle, then the value of radius will be mandatory  
        /// </summary>
        [Range(0.0, float.MaxValue, ErrorMessage = "The Circle Radius must grater than 0")]
        public float? Radius { get; set; }
        /// <summary>
        /// The shape type it is enum value
        /// The value of shape type my be one of the following:
        /// Line =1,
        /// Circle =2,
        /// Polygon =3,
        /// Triangle =4,
        /// Square =5
        /// </summary>
        /// <example>1</example>
        [Required(ErrorMessage = "The shape type is required")]
        [RegularExpression("^(1|2|3|4|5)$", ErrorMessage = "The value of Shape type must be one of the following 1,2,3,4,5")]
        public int ShapeType { get; set; }
        /// <summary>
        /// The shape points list represent the shape path 
        /// </summary>
        public List<PointViewModel> ShapePoints { get; set; }
    }
}
