using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Shared
{
    public class PaginationDto
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int ActualPage { get; set; }
        public int TotalPages { get; set; }
    }
}
