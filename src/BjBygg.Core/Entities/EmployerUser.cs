using BjBygg.SharedKernel;

namespace BjBygg.Core.Entities
{
    public class EmployerUser : UserEntity
    {
        public string EmployerId { get; set; }
        public Employer Employer { get; set; }
    }
}
