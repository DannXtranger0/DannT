using DannT.Models;
using DannT.Models.Context;
using DannT.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DannT.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthApi : ControllerBase
    {
        private readonly MyContext _context;
        public AuthApi(MyContext context)
        {
            _context = context;
        }
        //    public async Task<IActionResult> Login()
        //    {

        //    }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterDTO modelUser)
        {
            var existUser = _context.Users.FirstOrDefault(x => x.Email == modelUser.Email);

            if (existUser != null)
                return BadRequest("This email is already registered!");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(modelUser.Password);
            
            var user = new User
            {
                Email = modelUser.Email,
                HashedPassword = hashedPassword,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }
    }
}
