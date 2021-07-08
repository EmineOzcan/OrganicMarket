using System;
using System.Collections.Generic;
using System.Text;

namespace OrganicMarket.Core.Models
{
    public class AuthenticationResult
    {
        public string Token { get; set; }
        public bool Result { get; set; }
        public string ResultDescription { get; set; }
    }
}
