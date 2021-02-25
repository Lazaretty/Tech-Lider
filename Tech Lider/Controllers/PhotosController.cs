using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechLider.Models;
using Tech_Lider.Services_Api;

namespace TechLider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly DBContext dbContext;
        private readonly IApiService apiService;
        public PhotosController(DBContext context, IApiService service)
        {
            dbContext = context;
            apiService = service;
            apiService.InitDb(dbContext);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Photo>>> GetPhotos()
        {
            return await dbContext.Photos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Photo>> GetPhoto(int id)
        {
            var photo = await dbContext.Photos.FindAsync(id);

            if (photo == null)
            {
                return BadRequest();
            }

            return photo;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhoto(int id, Photo photo)
        {
           
            if (await apiService.PutPhotoService(photo) && id == photo.Id)
            {
                return Ok();
            }
            else
                return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<Photo>> PostPhoto(Photo photo)
        {
            if (await apiService.PostPhotoService(photo))
            {
                return Ok();
            }
            else 
                return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Photo>> DeletePhoto(int id)
        {
            if (await apiService.DeletePhotoService(id))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        
    }
}
