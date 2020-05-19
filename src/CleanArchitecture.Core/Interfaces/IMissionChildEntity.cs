using CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Core.Interfaces
{
    public interface IMissionChildEntity 
    {
        public Mission Mission { get; set; }
        public int MissionId { get; set; }
    }
}
