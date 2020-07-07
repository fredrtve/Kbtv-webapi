namespace BjBygg.Application.Common
{
    public class PaginationDto
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int ActualPage { get; set; }
        public int TotalPages { get; set; }
    }
}
