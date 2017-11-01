using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace api1.Authentication
{
    public class SecurityLevelRequirementFilter : IAuthorizationFilter
    {
        readonly int _actionSecurityLevel;
        ILogger _logger;

        public SecurityLevelRequirementFilter(ILogger<SecurityLevelRequirementFilter> logger, 
                int securityLevel)
        {
            _actionSecurityLevel = securityLevel;
            _logger = logger;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            LogDebugMessage("OnAutherization Method Called");
            var hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == "SecurityLevel");
            if (!hasClaim)
            {
                //If there is no security claims, result in Unauthorize
                string BearerError = "Claim missing";
                string BearerErrorDescription = "Executing method requires 'SecurityLevel' User Claim, It isn't provided in the token scopes";
                AddedBearerErrorHeader(context, BearerError, BearerErrorDescription);
                context.Result = new ForbidResult();
            }
            else
            {
                //If user has claim, check the security level is greater than action
                var userSecurityClaim = context.HttpContext.User.Claims.Where(c => c.Type == "SecurityLevel").FirstOrDefault();
                int userSecurityLevel = 0;
                if (Int32.TryParse(userSecurityClaim.Value, out userSecurityLevel))
                {
                    CheckUserSecurityLevel(context, userSecurityLevel);
                }
                else
                {
                    //Security Claim can't be parse to int
                    string BearerError = "Invalid Claim Type";
                    string BearerErrorDescription = "SecurityLevel User Claim is not of type Int32";
                    AddedBearerErrorHeader(context, BearerError, BearerErrorDescription);
                    context.Result = new ForbidResult();
                }
            }
            LogDebugMessage("OnAutherization Method Exited");
        }

        private void CheckUserSecurityLevel(AuthorizationFilterContext context, int userSecurityLevel)
        {
            if (userSecurityLevel < _actionSecurityLevel)
            {
                //User level is lesser than allowed
                //i.e. User has security level 1 and method requires security level 4
                string BearerError = "Insufficient SecurityLevel Claim";
                string BearerErrorDescription = "Executing method requires higher User Security Level to execute";
                AddedBearerErrorHeader(context, BearerError, BearerErrorDescription);
                context.Result = new ForbidResult();
            }
        }

        private void AddedBearerErrorHeader(AuthorizationFilterContext context, string BearerError, string BearerErrorDescription)
        {            
            string formatError = string.Format("Bearer error=\"{0}\", error_description=\"{1}\"", BearerError, BearerErrorDescription);
            Microsoft.Extensions.Primitives.StringValues authError = new Microsoft.Extensions.Primitives.StringValues(formatError);
            context.HttpContext.Response.Headers.Add("www-authenticate", authError);
            LogWarningMessage("Autherization Bearer Errror: " + formatError);
        }

        private void LogDebugMessage(string message, [CallerMemberName] string callerName = "")
        {
            if (_logger != null)
                message = callerName + ":" + message;
            _logger.LogDebug(message);
        }

        private void LogWarningMessage(string message, [CallerMemberName] string callerName = "")
        {
            if (_logger != null)
                message = callerName + ":" + message;
            _logger.LogWarning(message);
        }
    }
}
