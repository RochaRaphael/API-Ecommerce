﻿using API_Ecommerce.Models;
using System.Security.Claims;

namespace API_Ecommerce.Extensions
{
    public static class LoginClaimsExtension
    {
        public static IEnumerable<Claim> GetClaims(this User user)
        {
            var result = new List<Claim>
        {
            new(ClaimTypes.Name, user.Email)
        };
            result.AddRange(
                user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Slug))
            );
            return result;
        }
    }
}
