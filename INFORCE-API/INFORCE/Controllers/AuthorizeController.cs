
using INFORCE.Models;
using INFORCE.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ResponseMessage = INFORCE.Models.ResponseMessage;

namespace INFORCE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly IAuthorizeService _authorizeService;
        public AuthorizeController(IAuthorizeService authorizeService)
        {
            _authorizeService = authorizeService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegParam paramUser)
        {
            return Ok(await _authorizeService.SignUp(paramUser));
        }
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn1(UserLogParam paramUser)
        {
            return Ok(await _authorizeService.SignIn(paramUser));
        }

  
    }
}
