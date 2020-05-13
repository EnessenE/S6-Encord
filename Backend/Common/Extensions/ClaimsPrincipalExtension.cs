using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Encord.Common.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static string GetUserId(this ClaimsPrincipal claims)
        {
            return claims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
        }


        public static string GetName(this ClaimsPrincipal claims)
        {
            return claims.Claims.FirstOrDefault(c => c.Type == "sub").Value;
        }
    }

}
