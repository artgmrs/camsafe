using CamSafe.Business.Interfaces;
using CamSafe.Entity.CustomExceptions;
using CamSafe.Entity.DTOs;
using CamSafe.Entity.Entities;
using CamSafe.Repository.Interfaces;
using CamSafe.Utils.Validation;

namespace CamSafe.Business.Businesses
{
    public class CameraBusiness : ICameraBusiness
    {
        private readonly ICameraRepository _cameraRepository;

        public CameraBusiness(ICameraRepository cameraRepository)
        {
            _cameraRepository = cameraRepository;
        }

        public async Task<bool> CustomerHasGivenIpAsync(string cameraIp, string customerId)
        {
            var result = await _cameraRepository.CustomerHasGivenIpAsync(cameraIp, customerId);
            return result;
        }

        public async Task<Camera> CreateCameraAsync(CameraRequest requestParameters)
        {
            Validation.ValidateBoolean(requestParameters.IsEnabled);
            Validation.ValidateIpv4(requestParameters.Ip);

            var customerHasGivenIp = await CustomerHasGivenIpAsync(requestParameters.Ip, requestParameters.CustomerId);

            if (customerHasGivenIp) throw new CustomException($"Customer already has this camera's IP '{requestParameters.Ip}'.");

            var camera = new Camera()
            {
                Name = requestParameters.Name,
                Ip = requestParameters.Ip,
                IsEnabled = requestParameters.IsEnabled,
                CustomerId = requestParameters.CustomerId
            };

            var result = await _cameraRepository.InsertCameraAsync(camera);
            camera.Id = result;

            return camera;
        }

        public async Task<IEnumerable<Camera>> GetAllCamerasByFilterAsync(string? state, string? customerId)
        {
            IEnumerable<Camera> result = null;
            if (state != null)
            {
                Validation.ValidateBoolean(state);
                result = await _cameraRepository.GetAllCamerasByStateAsync(state);

            }

            if (customerId != null)
            {
                Validation.ValidateBoolean(customerId);
                result = await _cameraRepository.GetAllCamerasByCustomerIdAsync(customerId);

            }

            return result;
        }

        public async Task<CameraPatchResponse> ActivateCameraAsync(string cameraId)
        {
            Validation.ValidateInt(cameraId);

            await _cameraRepository.ActivateCameraAsync(cameraId);

            return new CameraPatchResponse(cameraId, "true");
        }

        public async Task<CameraPatchResponse> DeactivateCameraAsync(string cameraId)
        {
            Validation.ValidateInt(cameraId);

            await _cameraRepository.DeactivateCameraAsync(cameraId);

            return new CameraPatchResponse(cameraId, "false");
        }
    }
}
