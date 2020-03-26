using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLider.Models;

namespace Tech_Lider.Services_Api
{
   public interface IApiService
    {
        void InitDb(DBContext context);
        Task<bool> PostAlbumService(Album album);
        Task<bool> DeleteAlbumService(int id);
        Task<bool> DeleteAlbumService(Album album);
        Task<bool> PostPhotoService(Photo photo);
        Task<bool> PutPhotoService(Photo photo);
        Task<bool> DeletePhotoService(int id);
        Task RegisterService(User user);
    }
}
