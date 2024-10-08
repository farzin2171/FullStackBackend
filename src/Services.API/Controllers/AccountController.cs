using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.API.Dtos;
using Services.API.Entities;
using Services.API.Services;

namespace Services.API.Controllers;

    [Route("api/[controller]")]
    [ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<User> userManager;
    private readonly TokenService tokenService;

    public AccountController(UserManager<User> userManager, TokenService tokenService)
    {
        this.userManager = userManager;
        this.tokenService = tokenService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var user = await userManager.FindByNameAsync(loginDto.UserName);
        if (user == null || !await userManager.CheckPasswordAsync(user, loginDto.Password))
        {
            return Unauthorized();
        }

        return Ok(new UserDto
        {
            Email = user.Email,
            Token = await tokenService.GenerateToken(user),
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        var user = new User { UserName = registerDto.UserName, Email = registerDto.Email };

        var result = await userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError(item.Code, item.Description);
            }
            return ValidationProblem();
        }

        await userManager.AddToRoleAsync(user, "Memeber");

        return Created();
    }

    [Authorize]
    [HttpGet("currentUser")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var user = await userManager.FindByNameAsync(User.Identity.Name);

        return Ok(new UserDto { Email = user.Email, Token = await tokenService.GenerateToken(user) });
    }
}

