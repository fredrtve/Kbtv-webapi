using CleanArchitecture.Core.Entities;
using System;

namespace CleanArchitecture.Core.Specifications
{
    public class MissionSearchSpecification : BaseSpecification<Mission>
    {
        public MissionSearchSpecification(string searchString)
                : base(m => m.Address.Contains(searchString))
        {
            ApplyOrderByDescending(x => x.CreatedAt);
        }

    }
}
