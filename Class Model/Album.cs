using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechLider.Models
{
    public class Album
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
    }

    public class GetAlbum
    {
        public Album albumInfo { get; set; }
        public List<Photo> photos { get; set; }

        public GetAlbum(Album album, List<Photo> photos)
        {
            this.albumInfo = album;
            this.photos = photos;
        }
    }
}