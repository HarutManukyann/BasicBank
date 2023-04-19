using BL.Core;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Models.ChangePassword;
using Models.RestorePassword;

namespace WebAPI.Controllers
{

    [ApiController]
    public class ChangePasswordController : Controller
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserBL userBl;        
        private readonly ILogger<ChangePasswordController> logger;
        private int ActiveCode;

        public ChangePasswordController(UserBL userBl, IHttpContextAccessor httpContextAccessor,ILogger<ChangePasswordController> logger)
        {
            this.userBl = userBl;
            this.httpContextAccessor = httpContextAccessor;
            this.logger = logger;
        }

        [HttpPost]
        [Route("User/ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePassword pass)
        {
            var principal = httpContextAccessor.HttpContext.User;
            int userId =  Convert.ToInt32(principal.FindFirst("id").Value);
            var user = userBl.GetUserById(userId);
            if (user.Data.Password != pass.CurrentPassword)
            {
                return BadRequest("Not Found");
            }
            userBl.UpdatePass(pass.NewPassword, userId);
            logger.LogInformation($"[User with this ID: {userId}] change  password.");
            return Ok();
        }

        [HttpPost]
        [Route("User/VerifyPassword")]
        public async Task<IActionResult> VerifyPassword(string email)
        {
            Task.Delay(30000).ContinueWith(t => { ActiveCode = 0; });
            return Ok(ActiveCode = Convert.ToInt32(Guid.NewGuid()));
        }

        //[HttpPost]
        //[Route("VerifiEmail")]
        //public async Task<IActionResult> VerifyEmail(VerifyEmail verifyEmail)
        //{
        //    if (verifyEmail.VerifyCode != ActiveCode || ActiveCode == 0)
        //        return BadRequest();

        //    return Ok("Verifi is valid");
        //}

        [HttpPost]
        [Route("RestorePassword")]
        public async Task<IActionResult> RestorePassword(RestorePassword changePassword)
        {
            IEnumerable<Claim> claims = httpContextAccessor.HttpContext.User.Claims;
            var UserId = claims.FirstOrDefault(c => c.Type == "id");

            if (UserId == null)
            {
                return BadRequest("Not Found");
            }
            userBl.UpdatePassword(changePassword);
            return Ok("Restore is valid");
        }
    }
}
