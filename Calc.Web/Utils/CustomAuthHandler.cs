using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Calc.Web.Utils
{
    internal class CustomAuthHandler : AuthenticationHandler<CustomAuthOptions>
    {
        private const string HeaderName = "Token";
        private readonly HashSet<string> tokens = new HashSet<string>() { "AddToken", "MultiplicateToken" };

        public CustomAuthHandler(IOptionsMonitor<CustomAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {            
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Context.Request.Headers.TryGetValue(HeaderName, out var headerTokens) || !headerTokens.Any())
            {
                return Task.FromResult(AuthenticateResult.Fail("Token does not exist in header"));
            }

            var token = headerTokens.First();

            if (!tokens.Contains(token))
            {
                return Task.FromResult(AuthenticateResult.Fail("Token does not authorize"));
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, token)
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
