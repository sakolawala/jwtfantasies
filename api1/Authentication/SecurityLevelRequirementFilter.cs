using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api1.Authentication
{
    public class SecurityLevelRequirementFilter : IAuthorizationFilter
    {
        readonly int _actionSecurityLevel;

        public SecurityLevelRequirementFilter(int securityLevel)
        {
            _actionSecurityLevel = securityLevel;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == "SecurityLevel");
            if (!hasClaim)
            {
                //If there is no security claims, result in Unauthorize
                context.Result = new ForbidResult();
            }
            else
            {
                //If user has claim, check the security level is greater than action
                var userSecurityClaim = context.HttpContext.User.Claims.Where(c => c.Type == "SecurityLevel").FirstOrDefault();                
                var userSecurityLevel = Convert.ToInt32(userSecurityClaim.Value);
                if (userSecurityLevel < _actionSecurityLevel)
                {
                    //User level is lesser than allowed
                    //i.e. User has security level 1 and method requires security level 4
                    context.Result = new ForbidResult();
                }
            }
        }
    }
}
