using FindHelperApi.Models;
using FindHelperApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FindHelperApi.Controllers
{
    [Route("userAuthentication")]
    [ApiController]
    public class UserAuthenticationController : ControllerBase
    {

        private readonly UserAuthenticationService _userAuthenticationService;

        public UserAuthenticationController(UserAuthenticationService userAuthenticationService)
        {
            _userAuthenticationService = userAuthenticationService;
        }

        [HttpPost]
        [Route("login")]
        public ActionResult<User> Login(string email, string password)
        {
            var user = _userAuthenticationService.Login(email, password);
            return user;
        }
    }
}
