using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TechLider.Models;

namespace Tech_Lider.Services_Api
{
    public class ApiService : IApiService
    {
        private DBContext bdContext { get; set; }
        public void InitDb(DBContext context)
        {
            bdContext = context;
        }
        public async Task<bool> PostAlbumService(Album album)
        {
            if (IsPossibleToCreateAlbum(album.UserId))
            {
                bdContext.Albums.Add(album);
                await bdContext.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }

        public  async Task<bool> DeleteAlbumService(int id)
        {
            var album = await bdContext.Albums.FindAsync(id);
            if (album == null)
            {
                return false;
            }
            object p = bdContext.Albums.Remove(album);
            await bdContext.SaveChangesAsync();
            return true;
        }

        public  async Task<bool> DeleteAlbumService(Album album)
        {
            if (AlbumExists(album.Id))
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

        public  async Task<bool> PostPhotoService(Photo photo)
        {
            if (IsPossibleToAddPhoto(photo.AuthorId))
            {
                bdContext.Photos.Add(photo);
                await bdContext.SaveChangesAsync();
                return true;
            }
            else return false;
        }

        public  async Task<bool> PutPhotoService(Photo photo)
        {
            if (PhotoExists(photo.Id))
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

        public  async Task<bool> DeletePhotoService(int id)
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

        public async Task RegisterService(User user)
        {
            bdContext.Users.Add(user);
            await bdContext.SaveChangesAsync();
        }
        private bool AlbumExists(int id)
        {
            return bdContext.Albums.Any(e => e.Id == id);
        }

        private bool IsPossibleToCreateAlbum(int userId)
        {
            var usersAlbums = bdContext.Albums.Where(a => a.UserId == userId).ToList().Count();
            return usersAlbums <= 5;
        }

        private bool PhotoExists(int id)
        {
            return bdContext.Photos.Any(e => e.Id == id);
        }

        private bool IsPossibleToAddPhoto(int userId)
        {
            var usersAlbums = bdContext.Photos.Where(a => a.AuthorId == userId).ToList().Count();
            return usersAlbums <= 20;
        }
    }
}
