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
        private readonly IApiService apiService;

        public RegisterController(DBContext context, IApiService service)
        {
            bdContext = context;
            apiService = service;
            apiService.InitDb(bdContext);
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
            await apiService.RegisterService(user);
            return NotFound();
        }
    }
}
