using System;
using Microsoft.AspNetCore.Authorization;

namespace VailInstructorWikiApi.Auth
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        public RoleRequirement(AuthRole role)
        {
            Role = role;
        }

        public AuthRole Role { get; set; }
    }
}

