using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TechLider.Models;

namespace Tech_Lider.Services_Api
{
    public class ApiService : IApiService
    {
        public async Task<bool> PostAlbumService(DBContext bdContext, Album album)
        {
            if (IsPossibleToCreateAlbum(bdContext, album.UserId))
            {
                bdContext.Albums.Add(album);
                await bdContext.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }

        public  async Task<bool> DeleteAlbumService(DBContext bdContext, int id)
        {
            var album = await bdContext.Albums.FindAsync(id);
            if (album == null)
            {
                return false;
            }
            bdContext.Albums.Remove(album);
            await bdContext.SaveChangesAsync();
            return true;
        }

        public  async Task<bool> DeleteAlbumService(DBContext bdContext, Album album)
        {
            if (AlbumExists(bdContext, album.Id))
            {
                bdContext.Entry(album).State = EntityState.Modified;
                try
                {
                    await bdContext.SaveChangesAsync();
                }

                catch (DbUpdateConcurrencyException)
                {
                    return false;
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public  async Task<bool> PostPhotoService(DBContext dbContext, Photo photo)
        {
            if (IsPossibleToAddPhoto(dbContext, photo.AuthorId))
            {
                dbContext.Photos.Add(photo);
                await dbContext.SaveChangesAsync();
                return true;
            }
            else return false;
        }

        public  async Task<bool> PutPhotoService(DBContext bdContext, Photo photo)
        {
            if (PhotoExists(bdContext,photo.Id))
            {
                bdContext.Entry(photo).State = EntityState.Modified;
                try
                {
                    await bdContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return false;
                }
                return true;
            }
            else
                return false;
        }

        public  async Task<bool> DeletePhotoService(DBContext bdContext, int id)
        {
            var photo = await bdContext.Photos.FindAsync(id);
            {
                if (photo == null)
                {
                    return false;
                }
                bdContext.Photos.Remove(photo);
                await bdContext.SaveChangesAsync();

                return true;
            }
        }

        public async Task RegisterService(DBContext bdContext, User user)
        {
            bdContext.Users.Add(user);
            await bdContext.SaveChangesAsync();
        }
        private static bool AlbumExists(DBContext bdContext, int id)
        {
            return bdContext.Albums.Any(e => e.Id == id);
        }

        private bool IsPossibleToCreateAlbum(DBContext bdContext, int userId)
        {
            var usersAlbums = bdContext.Albums.Where(a => a.UserId == userId).ToList().Count();
            return usersAlbums <= 5;
        }

        private bool PhotoExists(DBContext bdContext, int id)
        {
            return bdContext.Photos.Any(e => e.Id == id);
        }

        private bool IsPossibleToAddPhoto(DBContext bdContext, int userId)
        {
            var usersAlbums = bdContext.Photos.Where(a => a.AuthorId == userId).ToList().Count();
            return usersAlbums <= 20;
        }
    }
}
