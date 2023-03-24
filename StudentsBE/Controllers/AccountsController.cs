using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StudentsBE.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentsBE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _config;

        public AccountsController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration)
        {
            _userManager= userManager;
            _signInManager= signInManager;
            _config= configuration;
        }
        
        [HttpPost("Create")]
        public async Task<ActionResult<AuthenticationResponse>> Create([FromBody] UserCredentials credentials)
        {
            var user = new IdentityUser { UserName = credentials.Email, Email = credentials.Email };

            var result= await _userManager.CreateAsync(user, credentials.Password);
            
            if(result.Succeeded)
            {
                return await BuildToken(credentials);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPost("LogIn")]
        public async Task<ActionResult<AuthenticationResponse>> Login([FromBody] UserCredentials credentials)
        {
            var result = await _signInManager.PasswordSignInAsync(credentials.Email, credentials.Password,
                isPersistent: false, lockoutOnFailure: false);

            if(result.Succeeded)
            {
                return await BuildToken(credentials);
            }
            else
            {
                return BadRequest("Login incorrecto");
            }
        }

            private async Task<AuthenticationResponse> BuildToken(UserCredentials credentials)
        {
            var claims = new List<Claim>()
            {
                new Claim("email", credentials.Email)
            };
            
            var user = await _userManager.FindByEmailAsync(credentials.Email);

            var claimsDb = await _userManager.GetClaimsAsync(user);

            claims.AddRange(claimsDb);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtKey"]));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddYears(1);

            var token = new JwtSecurityToken(issuer: null, audience: null, claims: claims, 
                expires: expiration, signingCredentials: cred);

            return new AuthenticationResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };


        }
    }
}
