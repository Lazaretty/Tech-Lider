using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechLider.Models;


namespace TechLider.Controllers
{
    [Route("api/[controller]")]
    public class RegisterController : Controller
    {
        private readonly DBContext bdContext;

        public RegisterController(DBContext context)
        {
            bdContext = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await bdContext.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Register(User user)
        { 
            bdContext.Users.Add(user);
            await bdContext.SaveChangesAsync();

            return Ok();
        }
    }
}
