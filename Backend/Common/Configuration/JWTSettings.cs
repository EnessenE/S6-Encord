using System;
using System.Collections.Generic;
using System.Text;

namespace Encord.Common.Configuration
{
    public class JWTSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public int ExpireDays { get; set; }
    }
}
