using CamSafe.Business.Interfaces;
using CamSafe.Entity.CustomExceptions;
using CamSafe.Entity.DTOs;
using CamSafe.Entity.Entities;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CamSafe.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerBusiness _customerBusiness;

        public CustomersController(ICustomerBusiness customerBusiness)
        {
            _customerBusiness = customerBusiness;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllCustomers()
        {
            var response = new GenericResponse<IEnumerable<Customer>>();

            try
            {
                var result = await _customerBusiness.GetAllCustomersAsync();
                response.Results = result;
                response.StatusCode = StatusCodes.Status200OK;

                return Ok(response);
            }
            catch (CustomException ex)
            {
                response.ErrorsMessages.Add(ex.Message);
                response.StatusCode = StatusCodes.Status400BadRequest;

                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response.ErrorsMessages.Add(ex.Message);
                response.StatusCode = StatusCodes.Status500InternalServerError;

                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomerById(string customerId)
        {
            var response = new GenericResponse<Customer>();

            try
            {
                var result = await _customerBusiness.GetCustomerById(customerId);
                response.Results = result;
                response.StatusCode = StatusCodes.Status200OK;

                return Ok(response);
            }
            catch (CustomException ex)
            {
                response.ErrorsMessages.Add(ex.Message);
                response.StatusCode = StatusCodes.Status400BadRequest;

                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response.ErrorsMessages.Add(ex.Message);
                response.StatusCode = StatusCodes.Status500InternalServerError;

                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerRequest requestParameters)
        {
            var response = new GenericResponse<Customer>();

            try
            {
                var result = await _customerBusiness.CreateCustomerAsync(requestParameters);
                response.Results = result;
                response.StatusCode = StatusCodes.Status201Created;

                return Created(new Uri(Request.GetEncodedUrl() + "/" + result.Id), response);
            }
            catch (CustomException ex)
            {
                response.ErrorsMessages.Add(ex.Message);
                response.StatusCode = StatusCodes.Status400BadRequest;

                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response.ErrorsMessages.Add(ex.Message);
                response.StatusCode = StatusCodes.Status500InternalServerError;

                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
