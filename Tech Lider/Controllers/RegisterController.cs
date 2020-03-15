using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechLider.Models;
using Tech_Lider.Services_Api;

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
        public async Task<ActionResult<IActionResult>> Register(User user)
        {
            await ApiService.RegisterService(bdContext, user);
            return NotFound();
        }
    }
}
