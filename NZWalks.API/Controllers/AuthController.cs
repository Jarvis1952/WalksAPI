using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {

            var identityUser = new IdentityUser
            {
                UserName = registerRequestDTO.userName,
                Email = registerRequestDTO.userName
            };

            var identityResult = await _userManager.CreateAsync(identityUser, registerRequestDTO.password);

            if(identityResult.Succeeded)
            {
                if(registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                {
                    identityResult = await _userManager.AddToRolesAsync(identityUser, registerRequestDTO.Roles);
                    
                    if (identityResult.Succeeded)
                    {
                        return Ok("User Created! Please Login");
                    }
                }
            }

            return BadRequest("Something Went wrong");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDTO.Username);

            if(user != null) 
            {
                var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

                if(isPasswordValid)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        var jwtToken = _tokenRepository.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResponseDTO
                        {
                            JwtToken = jwtToken,
                        };
                        return Ok(response);
                    }
                }
            }
            return BadRequest("Incorrect Username or Password");
        }
    }
}
