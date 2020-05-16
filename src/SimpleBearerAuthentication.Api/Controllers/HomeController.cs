using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleBearerAuthentication.Api.Domain.Entities;
using SimpleBearerAuthentication.Api.Domain.Services;
using SimpleBearerAuthentication.Api.Repository;
using System.Threading.Tasks;

namespace SimpleBearerAuthentication.Api.Controllers
{
    [Route("v1/account")]
    public class HomeController : ControllerBase
    {
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model)
        {
            var user = UserRepository.Get(model.Username, model.Password);
            if (user == null)
                return NotFound(new { message = "Username or password is invalid." });

            var token = TokenService.GenerateToken(user);
            user.Password = string.Empty;

            return new { user = user, token = token };
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anonymous";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => $"Authenticated - {User.Identity.Name}";

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "manager")]
        public string Manager() => $"Authenticated - {User.Identity.Name}";

        [HttpGet]
        [Route("account")]
        [Authorize(Roles = "manager, account")]
        public string Account() => $"Authenticated - {User.Identity.Name}";
    }
}