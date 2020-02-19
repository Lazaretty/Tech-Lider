using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechLider.Models;

namespace TechLider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {

        private readonly DBContext bdContext;
        public SearchController(DBContext context)
        {
            bdContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> Searh(string searchName, int id)
        {
            if (id == 1) {
                var result = await bdContext.Albums
                     .Where(a => a.Name == searchName)
                     .ToListAsync();
                return result;
            }
            else
            {
                var result = await bdContext.Photos
                     .Where(a => a.Name == searchName)
                     .ToListAsync();
                return result;
            }
            
        }
    }
}