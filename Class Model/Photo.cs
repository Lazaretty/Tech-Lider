namespace TechLider.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string ImageData { get; set; }
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string CameraName { get; set; }
        public string ShootingParameters { get; set; }
        public string Category { get; set; }
        public int AlbumID { get; set; }
    }
}