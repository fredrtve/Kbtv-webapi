using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Specifications
{
    public class EmployerByNameSpecification : BaseSpecification<Employer>
    {
        public EmployerByNameSpecification(string employerName)
                : base(m => m.Name == employerName)
        {

        }

    }
}
