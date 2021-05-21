using BjBygg.Application.Identity.Common;

namespace BjBygg.Application.Application.Common.Dto
{
    public class ApplicationUserDto : UserDto
    {
        public string EmployerId { get; set; }
    }
}