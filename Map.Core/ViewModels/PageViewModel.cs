﻿ using System.Collections.Generic;

 namespace Map.Core.ViewModels
{
    public class PageViewModel<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Length { get; set; }
        public IList<T> Data { get; set; }
    }
}
