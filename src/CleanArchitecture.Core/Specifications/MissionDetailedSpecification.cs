using CleanArchitecture.Core.Entities;

namespace CleanArchitecture.Core.Specifications
{
    public class MissionDetailedSpecification : BaseSpecification<Mission>
    {
        public MissionDetailedSpecification(int id)
            : base(m => m.Id == id)
        {
            AddInclude(m => m.MissionImages);
            AddInclude(m => m.MissionType);
            AddInclude(m => m.MissionNotes);
            AddInclude(m => m.Employer);
        }

    }
}
