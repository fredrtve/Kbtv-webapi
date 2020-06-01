using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CleanArchitecture.Core
{
    public class MissionPdfDto
    {
        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public Stream Image { get; set; }
    }
}
