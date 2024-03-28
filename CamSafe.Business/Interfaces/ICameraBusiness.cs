using CamSafe.Entity.DTOs;
using CamSafe.Entity.Entities;

namespace CamSafe.Business.Interfaces
{
    public interface ICameraBusiness
    {
        public Task<IEnumerable<Camera>> GetAllCamerasByStateAsync(string state);
        public Task<IEnumerable<Camera>> GetAllCamerasByCustomerIdAsync(string customerId);
        public Task<Camera> CreateCameraAsync(CameraRequest camera);
        public Task<CameraPatchResponse> ActivateCameraAsync(string cameraId);
        public Task<CameraPatchResponse> DeactivateCameraAsync(string cameraId);
    }
}
