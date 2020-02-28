using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechLider.Models;

namespace TechLider.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly DBContext bdContext;

        public PhotosController(DBContext context)
        {
            bdContext = context;
        }

        // GET: api/Photos
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Photo>>> GetPhotos()
        {
            return await bdContext.Photos.ToListAsync();
        }

        // GET: api/Photos/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Photo>> GetPhoto(int id)
        {
            var photo = await bdContext.Photos.FindAsync(id);

            if (photo == null)
            {
                return NotFound();
            }

            return photo;
        }

        // PUT: api/Photos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhoto(int id, Photo photo)
        {
           // if (int.Parse(User.Identity.Name) == photo.AuthorId)
           if(id == photo.Id)
            {
                if (id != photo.Id)
                {
                    return BadRequest();
                }

                bdContext.Entry(photo).State = EntityState.Modified;

                try
                {
                    await bdContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhotoExists(id))
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
            else return Unauthorized();
        }

        // POST: api/Photos
        [HttpPost]
        public async Task<ActionResult<Photo>> PostPhoto(Photo photo)
        {
            // if (IsPossibleToAddPhoto(int.Parse(User.Identity.Name)))
            if (IsPossibleToAddPhoto(photo.AuthorId))
            {
                bdContext.Photos.Add(photo);
                await bdContext.SaveChangesAsync();
                return CreatedAtAction("GetPhoto", new { id = photo.Id }, photo);
            }
            //else return Unauthorized();
            else return BadRequest();
        }

        // DELETE: api/Photos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Photo>> DeletePhoto(int id)
        {

            var photo = await bdContext.Photos.FindAsync(id);
            //if (int.Parse(User.Identity.Name) == photo.AuthorId)
            {
                if (photo == null)
                {
                    return NotFound();
                }

                bdContext.Photos.Remove(photo);
                await bdContext.SaveChangesAsync();

                return Accepted();
            }
           // else return Unauthorized();
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
