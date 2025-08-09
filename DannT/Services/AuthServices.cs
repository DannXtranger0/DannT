using DannT.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
namespace DannT.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        //Para acceder a HttpContext, se debe inyectar en program
        // builder.Services.AddHttpContextAccessor();

        public AuthServices(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async System.Threading.Tasks.Task Autenticate(User user,bool rememberMe = false)
        {
            //Claims
            var claimList = new List<Claim>
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim("UserId",user.Id.ToString())
            };

            if (user.GoogleId != null)
                claimList.Add(new Claim(ClaimTypes.NameIdentifier, user.GoogleId));

            if (user.Name != null)
                claimList.Add(new Claim(ClaimTypes.Name, user.Name));

    
            var claimsIdentity = new ClaimsIdentity(
                claimList, CookieAuthenticationDefaults.AuthenticationScheme);

            var authenticationProperties = new AuthenticationProperties
            {
                RedirectUri = "/",
                IsPersistent = rememberMe
            };

            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authenticationProperties);
        }
    }
}
