using DannT.Models;
using DannT.Models.Context;
using DannT.Models.DTO;
using DannT.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DannT.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthApi : ControllerBase
    {
        private readonly MyContext _context;
        private readonly IAuthServices _authServices;
        public AuthApi(MyContext context, IAuthServices authServices)
        {
            _context = context;
            _authServices = authServices;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]LoginDTO modelUser)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == modelUser.Email);
            if(user==null)
                return BadRequest("User Doesn't Exist");

            bool passwordMatch = BCrypt.Net.BCrypt.Verify(modelUser.Password, user.HashedPassword);
            if (!passwordMatch)
                return BadRequest("Wrong Password");

            await _authServices.Autenticate(user,modelUser.RememberMe);

            return Ok(user);
        }

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

            await _authServices.Autenticate(user);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpGet("GoogleLogin")]
        public IActionResult GoogleLogin(string returnUrl = "/")
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("SigninGoogle", "AuthApi", new { returnUrl })
            };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("SigninGoogle")]

        public async Task<IActionResult> SigninGoogle(string returnUrl = "/")
        {
            //Obtener los datos del login externo  de google
            var externalResult = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
            if (!externalResult.Succeeded)
                return RedirectToAction("GoogleLogin");

            //Extraer los claims del usuario
            var email = externalResult.Principal.FindFirst(ClaimTypes.Email)?.Value;
            var name = externalResult.Principal.FindFirst(ClaimTypes.Name)?.Value;
            var googleId= externalResult.Principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = _context.Users.FirstOrDefault(x => x.Email==email || x.GoogleId==googleId);

            if (user == null)
            {
                HttpContext.Session.SetString("Email", email);
                HttpContext.Session.SetString("Name", name);
                HttpContext.Session.SetString("GoogleId", googleId);

                return RedirectToAction("RequestPassword","Auth");
                //return Ok(new { status = "Request Password" });
            }

            return Ok();



            //if (user != null && user.GoogleId == null)
            //{
            //    user.GoogleId = googleId;
            //    user.Name = name;
            //    await _authServices.Autenticate(user);
            //    await _context.SaveChangesAsync();
            //}

        }
        [HttpPost("SavePassword")]
        public async Task<IActionResult> SavePassword([FromBody]RequestPasswordDTO passwordDTO)
        {
            if (string.IsNullOrWhiteSpace(passwordDTO.Password))
                return BadRequest(new { error = "Password vacío" });

            var user = new User
            {
                Name = HttpContext.Session.GetString("Name"),
                GoogleId= HttpContext.Session.GetString("GoogleId"),
                Email = HttpContext.Session.GetString("Email"),
                HashedPassword = BCrypt.Net.BCrypt.HashPassword(passwordDTO.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }
    }
}
