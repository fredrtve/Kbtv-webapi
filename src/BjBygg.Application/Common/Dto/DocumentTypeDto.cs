using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Common
{
    public class DocumentTypeDto : DbSyncDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
