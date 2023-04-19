using BL.Core;
using FCBankBasicHelper.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models;
using Models.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebAPI.Abstract;
using WebAPI.Token;

namespace WebAPI.Domain
{
    public class TokenService : ITokenService
    {
        private readonly TokenBL tokenBL;
        private readonly IConfiguration configuration;

        public TokenService(IConfiguration configuration, TokenBL tokenBL)
        {
            this.configuration = configuration;
            this.tokenBL = tokenBL;
        }

        public RefreshTokenRequest Refresh(RefreshTokenRequest tokenApiModel)
        {
            string accessToken = tokenApiModel.AccessToken;
            string refreshToken = tokenApiModel.RefreshToken;
            var principal = GetPrincipalFromExpiredToken(accessToken);
            var username = principal.Identity.Name;
            var user = tokenBL.GetTokens(username);
            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpire <= DateTime.Now)
                throw new Exception("invalid refresh token or user");
            var newAccessToken = GetToken(principal.Claims);
            var newRefreshToken = GetRefreshToken();
            user.RefreshToken = newAccessToken;
            tokenBL.Save();
            return new RefreshTokenRequest()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:key"])),
                ValidateLifetime = false,
            };
            var principal = new JwtSecurityTokenHandler().ValidateToken(token, tokenValidationParameters, out var securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid Token");
            return principal;
        }

        public string GetRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public string GetToken(IEnumerable<Claim> claims)
        {

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(configuration["Jwt:ExpiresInMinutes"])),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }

        public List<Claim> GetClaims(UserModel model)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("id", model.Id.ToString(), ClaimValueTypes.Integer32),
                new Claim(ClaimTypes.Name, model.Username)
            };
            return claims;
        }

    }
}
