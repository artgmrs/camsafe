namespace CamSafe.Entity.DTOs
{
    public class GenericResponse<T>
    {
        public T Results { get; set; }
        public List<string> ErrorsMessages { get; set; }
        public List<string> Messages { get; set; }
        public int? StatusCode { get; set; }

        public GenericResponse()
        {
            ErrorsMessages = new List<string>();
            Messages = new List<string>();
        }
    }
}
