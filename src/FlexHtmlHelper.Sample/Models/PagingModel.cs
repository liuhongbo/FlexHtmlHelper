using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexHtmlHelperSample.Models
{
    public class PagingModel
    {
        public int TotalItemCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int MaxPagingLinks { get; set; }
    }
}