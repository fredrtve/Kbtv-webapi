using BjBygg.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Core.Entities
{
    public class EmployerUser : UserEntity
    {
        public string EmployerId { get; set; }
        public Employer Employer { get; set; }
    }
}
