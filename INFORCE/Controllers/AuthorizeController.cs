
using INFORCE.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Response = INFORCE.Models.Response;

namespace INFORCE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _options;
        public AuthorizeController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration options)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _options = options;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegParam paramUser)
        {
            var userExists = await _userManager.FindByNameAsync(paramUser.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });
            var user = new IdentityUser()
            {
                UserName = paramUser.UserName,
                Email = paramUser.Email
            };
            var result = await _userManager.CreateAsync(user, paramUser.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim("Role", paramUser.Roles.ToString()));
                claims.Add(new Claim(ClaimTypes.Email, paramUser.Email));
                await _userManager.AddClaimsAsync(user, claims);
            }//w
            else
            {
                return BadRequest(new {Message="Some errors, pleace check your data"});
            }
            return Ok(new {Message = "User Registered!"});
        }
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(UserLogParam paramUser)
        {
            var user = await _userManager.FindByEmailAsync(paramUser.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, paramUser.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return Ok(new
                {
                    
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    Message = "Login Success"
                });
            }
            return NotFound(new { Message = "User not found" });
        }

        //private string GetToken(IdentityUser user, IEnumerable<Claim> prinicpal)
        //{
        //    var claims = prinicpal.ToList();
        //    claims.Add(new Claim(ClaimTypes.Name, user.UserName));
        //    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options["INFORCETEST)"]));
        //    var jwt = new JwtSecurityToken(
        //        issuer: _options["JWT:ValidIssuer"],
        //        audience: _options["JWT:ValidAudience"],
        //        claims: claims,
        //        expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)),
        //        notBefore: DateTime.UtcNow,
        //        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        //        );

        //    return new JwtSecurityTokenHandler().WriteToken(jwt);
        //}
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options["JWT:SecretKey"]));

            var token = new JwtSecurityToken(
                issuer: _options["JWT:ValidIssuer"],
                audience: _options["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
