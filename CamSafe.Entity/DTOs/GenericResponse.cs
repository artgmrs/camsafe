namespace CamSafe.Entity.DTOs
{
    public class GenericResponse<T>
    {
        public T Results { get; set; }
        public List<string> ErrorsMessages { get; set; } = new List<string>();
        public int? StatusCode { get; set; }
    }
}
