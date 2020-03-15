﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechLider.Models;
using Tech_Lider.Services_Api;
using Microsoft.AspNetCore.Authorization;
using System;

namespace TechLider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly DBContext bdContext;

        public AlbumsController(DBContext context)
        {
            bdContext = context;
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
            if (await ApiService.DeleteAlbumService(bdContext, album) && id == album.Id)
            {
                return NoContent()
            }
            else
            {
                return BadRequest();
            }
        }

       
        [HttpPost]
        public async Task<ActionResult<Album>> PostAlbum(Album album)
        {

            if (await ApiService.PostAlbumService(bdContext, album))
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
            if (await ApiService.DeleteAlbumService(bdContext, id)) 
            {
                return NoContent();
            }
            return BadRequest();
        }

    }
}
