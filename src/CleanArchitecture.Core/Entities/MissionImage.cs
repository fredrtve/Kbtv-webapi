using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.SharedKernel;
using System;

namespace CleanArchitecture.Core.Entities
{
    public class MissionImage : BaseEntity, IMissionChildEntity, IFile
    {
        public MissionImage() { }

        public Mission Mission { get; set; }
        public string MissionId { get; set; }
        public Uri FileUri { get; set; }
    }
}
