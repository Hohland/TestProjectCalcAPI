using System;
using Microsoft.AspNetCore.Authentication;

namespace Calc.Web.Utils
{
    public static class CustomAuthExtensions
    {
        public const string AuthenticationScheme = "Scheme";
        private const string DisplayName = "Auth";

        public static AuthenticationBuilder AddCustomAuth(this AuthenticationBuilder builder, Action<CustomAuthOptions> configureOptions)
        {
            return builder.AddScheme<CustomAuthOptions, CustomAuthHandler>(AuthenticationScheme, DisplayName, configureOptions);
        }
    }
}
