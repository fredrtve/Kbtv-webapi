using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Specifications
{
    public class MissionTypeByNameSpecification : BaseSpecification<MissionType>
    {
        public MissionTypeByNameSpecification(string typeName)
                : base(m => m.Name == typeName)
        {

        }

    }
}
