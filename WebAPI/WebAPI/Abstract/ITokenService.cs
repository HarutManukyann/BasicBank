using Models;
using Models.DTO;
using System.Security.Claims;
using WebAPI.Token;

namespace WebAPI.Abstract
{
    public interface ITokenService
    {
        List<Claim> GetClaims(UserModel model);
        RefreshTokenRequest Refresh(RefreshTokenRequest refreshTokenRequest);
        string GetToken(IEnumerable<Claim> claims);
        string GetRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
