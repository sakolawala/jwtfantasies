using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Security.Claims;

namespace api1.Authentication
{
    public static class NoAuthExtensions
    {
        public static AuthenticationBuilder AddNoAuthAuth(this AuthenticationBuilder builder, Action<NoAuthOptions> configureOptions)
        {
            return builder.AddScheme<NoAuthOptions, NoAuthHandler>("NoAuthScheme", "NoAuth", configureOptions);
        }
    }

    public class NoAuthOptions : AuthenticationSchemeOptions
    {
    }

    public class NoAuthHandler : AuthenticationHandler<NoAuthOptions>
    {
        public NoAuthHandler(IOptionsMonitor<NoAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
            // store custom services here...
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            
            Task<AuthenticateResult> rtnTask = Task<AuthenticateResult>.Factory.StartNew(() =>
            {
                NoAuthIdentity noauthIdentity = new NoAuthIdentity();
                System.Security.Claims.ClaimsIdentity ci = new System.Security.Claims.ClaimsIdentity();                
                System.Security.Principal.GenericIdentity gi = new System.Security.Principal.GenericIdentity("NoUser");
                System.Security.Claims.ClaimsPrincipal p = new System.Security.Claims.ClaimsPrincipal(noauthIdentity);
                AuthenticationTicket ticket = new AuthenticationTicket(p, "NoAuthScheme");
                return AuthenticateResult.Success(ticket);
            });
            return rtnTask;
        }
    }

    public class NoAuthIdentity : System.Security.Claims.ClaimsIdentity
    {
        public override bool IsAuthenticated => true;
        public override IEnumerable<Claim> Claims => new List<Claim>
        {
            new Claim("KiwiAuth", "SkipAuth")
        };
    }

}
