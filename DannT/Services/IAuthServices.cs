using DannT.Models;
using System.Security.Claims;

namespace DannT.Services
{
    public interface IAuthServices
    {
       public System.Threading.Tasks.Task Autenticate(User user,bool rememberMe = false);
    }
}
