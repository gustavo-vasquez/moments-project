using System;

namespace moments.Api.Resources
{
    public class JwtResource
    {
        public string Issuer { get; set; }
        public string Secret { get; set; }
        public int ExpirationInDays { get; set; }
    }
}