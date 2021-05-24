using BjBygg.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Core.Entities
{
    public class UserCommandStatus
    {
        public string UserName { get; set; }
        public string CommandId { get; set; }
        public bool HasSucceeded { get; set; }
    }
}
