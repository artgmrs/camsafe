using CamSafe.Business.Interfaces;
using CamSafe.Entity.CustomExceptions;
using CamSafe.Entity.DTOs;
using CamSafe.Entity.Entities;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CamSafe.Controllers
{
    [ApiController]
    [Route("api/cameras")]
    public class CamerasController : ControllerBase
    {
        private readonly ICameraBusiness _cameraBusiness;

        public CamerasController(ICameraBusiness cameraBusiness)
        {
            _cameraBusiness = cameraBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCamerasByState(string state, string customerId)
        {
            var response = new GenericResponse<IEnumerable<Camera>>();

            try
            {
                var result = await _cameraBusiness.GetAllCamerasByFilterAsync(state, customerId);
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
        public async Task<IActionResult> CreateCamera(CameraRequest requestParameters)
        {
            var response = new GenericResponse<Camera>();

            try
            {
                var result = await _cameraBusiness.CreateCameraAsync(requestParameters);
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

        [HttpPatch("activate/{cameraId}")]
        public async Task<IActionResult> ActivateCamera(string cameraId)
        {
            var response = new GenericResponse<CameraPatchResponse>();

            try
            {
                var result = await _cameraBusiness.ActivateCameraAsync(cameraId);
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

        [HttpPatch("deactivate/{cameraId}")]
        public async Task<IActionResult> DeactivateCamera(string cameraId)
        {
            var response = new GenericResponse<CameraPatchResponse>();

            try
            {
                var result = await _cameraBusiness.DeactivateCameraAsync(cameraId);
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
    }
}
