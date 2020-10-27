using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.SharedKernel
{
    public interface IContactable
    {
        string PhoneNumber { get; set; }
        string Email { get; set; }
    }
}
