namespace CamSafe.Entity.Entities
{
    public class Camera
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ip { get; set; }
        public string IsEnabled { get; set; }
        public string CustomerId { get; set; }
    }
}