using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechLider.Models;
using Microsoft.AspNetCore.Authorization;

namespace TechLider.Controllers
{
    [Route("api/[controller]")]
   // [Authorize]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly DBContext bdContext;

        public AlbumsController(DBContext context)
        {
            bdContext = context;
        }

        // GET: api/Albums
        //[AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Album>>> GetAlbums()
        {
            return await bdContext.Albums.ToListAsync();
        }

        // GET: api/Albums/5
        [HttpGet("{id}")]
       // [AllowAnonymous]
        public async Task<ActionResult<Album>> GetAlbum(int id)
        {
            var album = await bdContext.Albums.FindAsync(id);

            if (album == null)
            {
                return NotFound();
            }

            return album;
        }

        // PUT: api/Albums/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlbum(int id, Album album)
        {
            //if (int.Parse(User.Identity.Name) == album.UserId)
            {
                if (id != album.Id)
                {
                    return BadRequest();
                }

                bdContext.Entry(album).State = EntityState.Modified;

                try
                {
                    await bdContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return Accepted();
            }
           // else return Unauthorized();

        }

        // POST: api/Albums
       
        [HttpPost]
        public async Task<ActionResult<Album>> PostAlbum(Album album)
        {
            // if (IsPossibleToCreateAlbum(int.Parse(User.Identity.Name
            if (IsPossibleToCreateAlbum(album.UserId))
            {
                // album.UserId = int.Parse(User.Identity.Name);
                bdContext.Albums.Add(album);
                await bdContext.SaveChangesAsync();

                return CreatedAtAction("GetAlbum", new { id = album.Id }, album);
            }
            else
                // return Unauthorized();
                return BadRequest();
        }

        // DELETE: api/Albums/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Album>> DeleteAlbum(int id)
        {
            var album = await bdContext.Albums.FindAsync(id);
         //   if (int.Parse(User.Identity.Name) == album.UserId)
            {
                if (album == null)
                {
                    return NotFound();
                }

                bdContext.Albums.Remove(album);
                await bdContext.SaveChangesAsync();
                
                return Accepted();
            }
          //  else 
            {
                return Unauthorized();
            }
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

    }
}
