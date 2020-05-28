using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.SharedKernel;
using System;

namespace CleanArchitecture.Core.Entities
{
    public class MissionImage : BaseEntity, IMissionChildEntity
    {
        public MissionImage() {}

        public Mission Mission { get; set; }
        public int MissionId { get; set; }
        public Uri FileURL { get; set; }
    }
}
