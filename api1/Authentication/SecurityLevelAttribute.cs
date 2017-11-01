using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api1.Authentication
{
    public class SecurityLevelAttribute : TypeFilterAttribute
    {
        public SecurityLevelAttribute(int securityLevel) : base(typeof(SecurityLevelRequirementFilter))
        {
            Arguments = new object[] { securityLevel };
        }
    }
}
