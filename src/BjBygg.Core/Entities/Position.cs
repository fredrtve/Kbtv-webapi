using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Core.Entities
{
    public class Position
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsExact { get; set; }
    }
}
