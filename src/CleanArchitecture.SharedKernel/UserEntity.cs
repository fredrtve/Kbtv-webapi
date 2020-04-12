using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.SharedKernel
{
    public abstract class UserEntity : BaseEntity
    {
        public string UserName { get; set; }
    }
}
