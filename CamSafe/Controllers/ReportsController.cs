using CamSafe.Business.Interfaces;
using CamSafe.Entity.CustomExceptions;
using CamSafe.Entity.DTOs;
using CamSafe.Entity.Entities;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CamSafe.Controllers
{
    [Route("api/reports")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportBusiness _reportBusiness;

        public ReportsController(IReportBusiness reportBusiness)
        {
            _reportBusiness = reportBusiness;
        }

        [HttpGet()]
        public async Task<IActionResult> GetReportsWithFilters([FromQuery] ReportFilterRequest requestParameters)
        {
            var response = new GenericResponse<IEnumerable<Report>>();

            try
            {
                var result = await _reportBusiness.GetReportsWithFiltersAsync(requestParameters);
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
        public async Task<IActionResult> CreateReport(ReportRequest requestParameters)
        {
            var response = new GenericResponse<Report>();

            try
            {
                var result = await _reportBusiness.CreateReportAsync(requestParameters);
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
