using CleanArchitecture.Core.Entities;

namespace CleanArchitecture.Core.Specifications
{
    public class MissionPaginatedSpecification : BaseSpecification<Mission>
    {
        public MissionPaginatedSpecification(int skip, int take)
            : base(m => true)
        {
            ApplyOrderByDescending(x => x.CreatedAt);
            ApplyPaging(skip, take);      
        }

    }
}
