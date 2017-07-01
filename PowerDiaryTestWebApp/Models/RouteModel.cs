namespace PowerDiaryTestWebApp.Models
{
    public class RouteModel
    {
        public int Id { get; set; }
        public string Uri { get; set; }
        public string CompressedUri { get; set; }

        public string CompressedUriUserValue { get; set; }
    }
}