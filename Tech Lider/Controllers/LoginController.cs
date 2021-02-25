using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechLider.Models;
using Microsoft.AspNetCore.Http;


namespace TechLider.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly DBContext bdContext;
        public LoginController(DBContext context)
        {
            bdContext = context;
        }

        [HttpPost]
        public async Task<IActionResult> Login(User model)
        {
           
            var user = await bdContext.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
            if (user != null)
            {
                await Authenticate(user.Id.ToString());
                return Ok();
            }

            return BadRequest();

        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }

        private async Task Authenticate(string userId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userId)
            };
   
            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

    }
}
