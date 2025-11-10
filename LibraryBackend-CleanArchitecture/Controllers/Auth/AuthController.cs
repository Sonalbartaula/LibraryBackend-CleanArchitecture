
using LibraryBackend_CleanArchitecture.Entities;
using LibraryBackend_CleanArchitecture.Model;
using LibraryBackend_CleanArchitecture.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBackend_CleanArchitecture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService service;


        public AuthController(IAuthService service)
        {
            this.service = service;

        }


        [HttpPost("register")]
        public async Task<ActionResult<User?>> Register(UserDto request)
        {
            var user = await service.RegisterAsync(request);
            if (User is null)

                return BadRequest("User already exists.");

            user.Username = request.Username;
            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, request.Password);
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> login(UserDto request)
        {
            var token = await service.LoginAsync(request);
            if (token is null)
                return BadRequest("Username/Password is wrong");
            return Ok(token);

        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto request)
        {
            var token = await service.RefreshTokenAsync(request);
            if (token is null)
                return BadRequest("Invalid/Expired Token");
            return Ok(token);

        }
        [HttpGet("Auth-Endpoint")]
        [Authorize]
        public IActionResult AuthCheck()
        {
            return Ok("You are authorized to access this endpoint.");
        }

        [HttpGet("Admin-Endpoint")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminCheck()
        {
            return Ok("You are authorized to access this endpoint.");
        }

    }
}