using System;
using System.ComponentModel.DataAnnotations;

namespace Map.Core.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class PageParamsList
    {
        /// <summary>
        /// 
        /// </summary>
        [Range(1,int.MaxValue,ErrorMessage = "The Value of current page must be grater than 0")]
        public int CurrentPage { get; set; } = 1;
        /// <summary>
        /// 
        /// </summary>
        [Range(1,200,ErrorMessage = "The Value od page size must be grater than 0 and less than 200")]

        public int PageSize { get; set; } = 10;
    }
}
