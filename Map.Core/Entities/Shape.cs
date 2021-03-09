using System.Collections.Generic;
using Map.Core.Enum;
using Map.Core.Shared;

namespace Map.Core.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class Shape : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public Shape() => ShapePoints = new HashSet<Point>();
        /// <summary>
        /// The Shape Title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The color will fill into shape
        /// </summary>
        public string FillColorCode { get; set; }
        /// <summary>
        /// The filled color opacity
        /// </summary>
        public double FillOpacity { get; set; }
        /// <summary>
        /// The shape  stroke weight and the value must
        /// be between 0.0 and 1.0 and the default 
        /// </summary>
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
        public int ZoomRatio { get; set; }
        /// <summary>
        /// if the shape is circle, then the value of radius will be mandatory  
        /// </summary>
        public float? Radius { get; set; }
        /// <summary>
        /// The shape type it is enum value 
        /// </summary>
        public ShapeType ShapeType { get; set; }
        /// <summary>
        /// The shape points list represent the shape path 
        /// </summary>
        public ICollection<Point> ShapePoints { get; set; }
    }
}
