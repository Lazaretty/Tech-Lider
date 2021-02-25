using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.CodeGeneration;
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
        public async Task<ActionResult<IEnumerable<GetAlbum>>> GetAlbums()
        {
            var albums =  await bdContext.Albums.ToListAsync();
            List<GetAlbum> resGetAlbums = new List<GetAlbum>();
            foreach (var album in albums)
            {
                resGetAlbums.Add(new GetAlbum(album,
                    await bdContext.Photos.Select(x => x).Where(x => x.AlbumID == album.Id).ToListAsync()));
            }

            return resGetAlbums;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetAlbum>> GetAlbum(int id)
        {
            var album = await bdContext.Albums.FindAsync(id);
            if (album == null)
            {
                return NoContent();
            }

            GetAlbum resGetAlbum = new GetAlbum(album,
                await bdContext.Photos.Select(x => x).Where(x => x.AlbumID == album.Id).ToListAsync());
            return resGetAlbum;
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
                return Ok();
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
                return Ok();
            }
            return BadRequest();
        }

    }
}
