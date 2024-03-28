using CamSafe.Entity.Entities;

namespace CamSafe.Repository.Interfaces
{
    public interface ICameraRepository
    {
        public Task<IEnumerable<Camera>> GetAllCamerasByStateAsync(string state);
        public Task<IEnumerable<Camera>> GetAllCamerasByCustomerIdAsync(string customerId);
        public Task<int> InsertCameraAsync(Camera camera);
        public Task ActivateCameraAsync(string cameraId);
        public Task DeactivateCameraAsync(string cameraId);
        public Task<bool> CustomerHasGivenIpAsync(string cameraIp, string customerId);
    }
}
