namespace CamSafe.Entity.DTOs
{
    public record CameraRequest(string Name, string Ip, string IsEnabled, string CustomerId);
    public record CustomerRequest(string Name);
    public record ReportFilterRequest(string? InitialDate, string? EndDate, string? CustomerId, string? dateHour);
    public record ReportRequest(string OccuredAt, string CameraId);
    public record CameraPatchResponse(string CameraId, string IsEnabled);
}
