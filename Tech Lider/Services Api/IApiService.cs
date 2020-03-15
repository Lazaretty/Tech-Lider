using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLider.Models;

namespace Tech_Lider.Services_Api
{
   public interface IApiService
    {
        Task<bool> PostAlbumService(DBContext bdContext, Album album);
        Task<bool> DeleteAlbumService(DBContext bdContext, int id);
        Task<bool> DeleteAlbumService(DBContext bdContext, Album album);
        Task<bool> PostPhotoService(DBContext dbContext, Photo photo);
        Task<bool> PutPhotoService(DBContext bdContext, Photo photo);
        Task<bool> DeletePhotoService(DBContext bdContext, int id);
        Task RegisterService(DBContext bdContext, User user);
    }
}
