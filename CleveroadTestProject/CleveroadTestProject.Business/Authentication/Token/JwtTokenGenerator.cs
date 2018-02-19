namespace CleveroadTestProject.Business.Authentication
{
    #region namespaces
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using CleveroadTestProject.Infrastructure;
    using CleveroadTestProject.Infrastructure.Extensions;
    using CleveroadTestProject.ViewModel.Authentication;
    using Microsoft.IdentityModel.Tokens;
    using Newtonsoft.Json;
    #endregion

    public class JwtTokenGenerator : ITokenGenerator
    {
        public string GenerateNewToken()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        public TokenResponse GenerateSecurityToken(AuthenticationSettings settings, string refreshToken)
        {
            DateTime timestamp = DateTime.UtcNow;
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, timestamp.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
            };

            var symmetricKeyAsBase64 = settings.Secret;
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);

            var jwt = new JwtSecurityToken(
                claims: claims,
                notBefore: timestamp,
                expires: timestamp.AddSeconds(settings.AccessTokenLifespan),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new TokenResponse()
            {
                AccessToken = encodedJwt,
                ExpiresIn = settings.AccessTokenLifespan,
                RefreshToken = refreshToken,
            };

            return response;
        }
    }
}
