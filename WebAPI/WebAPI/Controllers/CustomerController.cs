using BL.Core;
using FCBankBasicHelper.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Models.BaseType;
using Models.DTO;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly CustomerBl customerBl;
        private readonly ILogger<CustomerController> logger;
        public CustomerController(ILogger<CustomerController> logger, CustomerBl customerBl)
        {
            this.logger = logger;
            this.customerBl = customerBl;
        }
        [HttpPost]
        [Route("Post")]
        public async Task<IActionResult> AddCustomer([FromBody] CustomerModel model)
        {
            if (model == null) return BadRequest(ModelState);
            ResponseBase<CustomerModel> response = customerBl.InsertCustomer(model);
            logger.LogInformation(response.Message);
            return Ok(response);
        }
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerModel customer)
        {
            ResponseBase<CustomerModel> response = customerBl.UpdateCustomer(customer);
            logger.LogInformation(response.Message);
            return Ok(response);
        }
        [HttpGet]
        [Route("GetbyPassport")]
        public async Task<IActionResult> GetCustomerByPassport(string passport)
        {
            ResponseBase<CustomerModel> response = customerBl.GetCustomerByPassport(passport);
            logger.LogInformation(response.Message);
            return Ok(response);
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var response = customerBl.GetAll();
            logger.LogInformation(response.Message);

            return Ok(response);
        }
        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            ResponseBase<CustomerModel> response = customerBl.GetCustomerById(id);
            logger.LogInformation(response.Message);
            return Ok(response);
        }
        [HttpDelete]
        [Route("Remove")]
        public async Task<IActionResult> RemoveCustomer([FromBody] string passport)

        {
            ResponseBase<CustomerModel> response = customerBl.RemoveCustomer(passport);
            logger.LogInformation(response.Message);
            return Ok(response);
        }

        [HttpPost]
        [Route("CustomerFiltration")]
        public async Task<IActionResult> CustomerFiltration([FromBody] FiltrationModel filtrationModel)
        {
            try
            {
                var customers = customerBl.GetFilteredCustomers(filtrationModel);
                return Ok(customers);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while getting the customer list.");
                throw;
            }
        }
    }
}