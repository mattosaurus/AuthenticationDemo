using Microsoft.Identity.Web;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationDemo.WeatherForecast.Authenticators
{
    public class TokenAuthenticator : AuthenticatorBase
    {
        private readonly ITokenAcquisition _tokenAcquisition;
        private readonly string[] _scopes;

        public TokenAuthenticator(ITokenAcquisition tokenAcquisition, string[] scopes) : base("")
        {
            _tokenAcquisition = tokenAcquisition;
            _scopes = scopes;
        }

        protected override async ValueTask<Parameter> GetAuthenticationParameter(string accessToken)
        {
            var token = string.IsNullOrEmpty(Token) ? await _tokenAcquisition.GetAccessTokenForUserAsync(_scopes) : Token;
            return new HeaderParameter(KnownHeaders.Authorization, $"Bearer {token}");
        }
    }
}
