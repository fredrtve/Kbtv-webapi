using BjBygg.Application.Identity.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Identity.Common
{
    public class CreateTokensResponse
    {
        public string RefreshToken { get; set; }
        public AccessToken AccessToken { get; set; }
    }
}
