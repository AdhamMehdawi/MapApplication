 

using System.ComponentModel;

namespace Map.Core.Enum
{
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
    public enum ShapeType
    {
        /// <summary>
        /// the line shape
        /// </summary>
        [Description("Line")]
        Line =1,
        /// <summary>
        /// the circle shape 
        /// </summary>
        [Description("Circle")]
        Circle =2,
        /// <summary>
        /// the polygon shape
        /// </summary>
        [Description("Polygon")]
        Polygon =3,
        /// <summary>
        /// the triangle shape
        /// </summary>
        [Description("Triangle")]
        Triangle =4,
        /// <summary>
        /// the square shape
        /// </summary>
        [Description("Square")]
        Square =5
    }
}
