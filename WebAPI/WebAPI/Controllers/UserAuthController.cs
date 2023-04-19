using BL.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Serilog;
using WebAPI.Abstract;
using WebAPI.Domain;

namespace WebAPI.Controllers
{
    [ApiController]
    public class UserAuthController : ControllerBase
    {
        private readonly ITokenService tokenService;
        private readonly TokenBL tokenBL;
        private readonly UserBL userBl;
        private readonly ILogger<UserAuthController> logger;

        public UserAuthController( ITokenService tokenService, ILogger<UserAuthController> logger, UserBL userBl, TokenBL tokenBL)
        {
            this.tokenService = tokenService;
            this.tokenBL = tokenBL;
            this.userBl = userBl;
            this.logger = logger;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            var model = userBl.GetUser(login.Email, login.Password);
            if (model is null)
            {
                throw new Exception("Mail or password is incorrect");
            }
            var authClaims = tokenService.GetClaims(model.Data);
            var JWTtoken = tokenService.GetToken(authClaims);
            var refreshToken = tokenService.GetRefreshToken();
            tokenBL.Save(refreshToken, model.Data.Username);
            logger.LogInformation($"[Username:{model.Data.Username}] was logined seccessfully.");
            return Ok(new AuthenticatedResponse
            {
                Token = JWTtoken,
                RefreshToken = refreshToken
            });
        }

        [HttpPost]
        [Route("Registration")]
        public async Task<IActionResult> Registration([FromBody] UserModel model)
        {
            if (tokenBL.UserValid(model))
            {
                var user = new IdentityUser
                {
                    UserName = model.Username,
                    Email = model.Email
                };
                var response=userBl.InsertUser(model);
                if (response.Success==false)
                {
                    throw new Exception(response.Message);
                }
                var authClaims = tokenService.GetClaims(model);
                var token = tokenService.GetToken(authClaims);
                var refreshToken = tokenService.GetRefreshToken();
                tokenBL.Save(refreshToken, user.UserName);
                logger.LogInformation($"[Username:{model.Username}] was rigistered seccessfully.");
                return Ok(new AuthenticatedResponse
                {
                    Token = token,
                    RefreshToken = refreshToken
                });
            }
            throw new Exception("Registration failed");
        }
    }
}
