using BL.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using System.Data;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserBL userBl;
        private readonly RoleBl roleBl;
        private readonly ILogger<UserController> logger;
        public UserController(ILogger<UserController> logger , UserBL userBl,RoleBl roleBl)
        {
            this.userBl = userBl;
            this.logger = logger;
            this.roleBl = roleBl;
            logger.LogInformation(Environment.NewLine + "\nUser controller called ");
        }
       
        [HttpPost]
        [Route("Post")]
          public async Task<IActionResult> AddUser([FromBody] UserModel model)
        {
            //var user = GetCurrentUser();
            //if (!userBl.HasPermission(user.Username, "Admin"))
            //{
            //    throw new Exception("You have not permission");
            //}
            if (model == null) return BadRequest(ModelState);
            var response = userBl.InsertUser(model);
            logger.LogInformation(response.Message);
            return Ok(response);
        }

        [HttpPost]
        [Route("AddRoleForUser")]
        public async Task<IActionResult> AddRoleForUser([FromBody] UserRolesMappingModel model)
        {
            //var user = GetCurrentUser();
            //if (!userBl.HasPermission(user.Username, "Admin"))
            //{
            //    throw new Exception("You have not permission");
            //}
            if (model == null) return BadRequest(ModelState);
            var response = roleBl.AddRoleForUser(model);
            logger.LogInformation(response.Message);
            return Ok(response);
        }

        private UserModel GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new UserModel
                {
                    Username = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
                };
            }
            return null;
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateUser([FromBody] UserModel userInfo)
        {
            var response = userBl.UpdateUser(userInfo);
            logger.LogInformation(response.Message);
            return Ok(response);

        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetUser([FromBody] LoginModel model)
        {
            //var user = GetCurrentUser();
            //if (!userBl.HasPermission(user.Username, "Admin"))
            //{
            //    throw new Exception("You have not permission");
            //}
            var response = userBl.GetUser(model.Email, model.Password);
            logger.LogInformation(response.Message);
            return Ok(response);

        }

        [HttpDelete]
        [Route("Remove")]        
        public async Task<IActionResult> RemoveUser(string username)
        {
            var response = userBl.RemoveUser(username);
            logger.LogInformation(response.Message);
            return Ok(response);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var response = userBl.GetUserById(id);
            logger.LogInformation(response.Message);
            return Ok(response);
        }

    }

}
