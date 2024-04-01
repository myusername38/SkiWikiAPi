using System;
using Microsoft.AspNetCore.Authorization;

namespace VailInstructorWikiApi.Auth
{
    public class AnyRoleRequirement : IAuthorizationRequirement
    {
        public AnyRoleRequirement(List<AuthRole> roles)
        {
            Roles = roles;
        }

        public List<AuthRole> Roles { get; set; }
    }
}

