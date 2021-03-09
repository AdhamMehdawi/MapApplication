using System;
using System.Collections.Generic;
using System.Text;
using Map.Core.Shared;

namespace Map.Core.Entities
{
    public class Point : BaseEntity
    {
        /// <summary>
        /// The latitude 
        /// </summary>
        public double Lat { get; set; }
        /// <summary>
        /// The longitude
        /// </summary>
        public double Lng { get; set; }
        /// <summary>
        /// The foreigner to shape 
        /// </summary>
        public int ShapeId { get; set; }
        /// <summary>
        /// The navigation property  to shape entity 
        /// </summary>
        public Shape Shape { get; set; }
    }
}
