using CamSafe.Entity.DTOs;
using CamSafe.Entity.Entities;

namespace CamSafe.Business.Interfaces
{
    public interface ICameraBusiness
    {
        public Task<IEnumerable<Camera>> GetAllCamerasByFilterAsync(string? state, string? customerId);
        public Task<Camera> CreateCameraAsync(CameraRequest camera);
        public Task<CameraPatchResponse> ActivateCameraAsync(string cameraId);
        public Task<CameraPatchResponse> DeactivateCameraAsync(string cameraId);
    }
}
