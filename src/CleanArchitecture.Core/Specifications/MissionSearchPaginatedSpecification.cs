using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Specifications
{
    public class MissionSearchPaginatedSpecification : BaseSpecification<Mission>
    {
        public MissionSearchPaginatedSpecification(int skip, int take, string searchString)
            : base(m => m.Address.Contains(searchString))
        {
            ApplyOrderByDescending(x => x.CreatedAt);
            ApplyPaging(skip, take);
        }

    }
}
