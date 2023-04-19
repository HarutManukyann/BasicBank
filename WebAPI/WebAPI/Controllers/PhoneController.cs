using BL.Core;
using FCBankBasicHelper.Models;
using Microsoft.AspNetCore.Mvc;
using Models.BaseType;
using Models.DTO;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneController : Controller
    {
        private readonly PhoneBl phonebl ;
        private readonly ILogger<PhoneController> logger;
        public PhoneController(ILogger<PhoneController> logger, PhoneBl phonebl)
        {
            this.logger = logger;
            this.phonebl = phonebl;
        }

        [HttpPost]
        [Route("Post")]
        public async Task<IActionResult> AddPhone([FromBody] PhoneModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            ResponseBase<PhoneModel> response = phonebl.Insertphone(model);
            logger.LogInformation(response.Message);
            return Ok(response);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateUsers([FromBody] PhoneModel model)
        {
            ResponseBase<PhoneModel> response = phonebl.Updatephone(model);
            logger.LogInformation(response.Message);
            return Ok(response);
        }

        [HttpGet]
        [Route("GetByUserId")]
        public async Task<IActionResult> GetUser(int userid)
        {
            ResponseBase<PhoneModel> response = phonebl.GetPhone(userid);
            logger.LogInformation(response.Message);
            return Ok(response);
        }

        [HttpDelete]
        [Route("Remove")]
        public async Task<IActionResult> RemoveUser([FromBody] PhoneModel model)
        {
            ResponseBase<PhoneModel> response = phonebl.Deletephone(model);
            logger.LogInformation(response.Message);
            return Ok(response);
        }
    }
}
