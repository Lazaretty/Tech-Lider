using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tech_Lider.Services_Api;
using TechLider.Models;

namespace TechLider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly DBContext bdContext;
        private readonly IApiService apiService;

        public AlbumsController(DBContext context, IApiService service)
        {
            bdContext = context;
            apiService = service;
            apiService.InitDb(bdContext);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Album>>> GetAlbums()
        {
            return await bdContext.Albums.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Album>> GetAlbum(int id)
        {
            var album = await bdContext.Albums.FindAsync(id);
            if (album == null)
            {
                return NoContent();
            }

            return album;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlbum(int id, Album album)
        {
            if (await apiService.DeleteAlbumService(album) && id == album.Id)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

       
        [HttpPost]
        public async Task<ActionResult<Album>> PostAlbum(Album album)
        {

            if (await apiService.PostAlbumService(album))
            {
                return NoContent();
            }
            else
            {
                return BadRequest(); 
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Album>> DeleteAlbum(int id)
        {
            if (await apiService.DeleteAlbumService(id)) 
            {
                return NoContent();
            }
            return BadRequest();
        }

    }
}
