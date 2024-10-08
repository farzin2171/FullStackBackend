using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Services.API.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.API.Services;

public class TokenService
{
    private readonly UserManager<User> userManager;
    private readonly IConfiguration configuration;

    public TokenService(UserManager<User> userManager, IConfiguration configuration)
    {
        this.userManager = userManager;
        this.configuration = configuration;
    }

    public async Task<string> GenerateToken(User user)
    {
        var claimes = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name,user.UserName)
            };

        var roles = await userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            claimes.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:TokenKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenOptions = new JwtSecurityToken(
            issuer: null,
            audience: null,
            claims: claimes,
            expires: DateTime.Now.AddDays(7),
            signingCredentials: creds
            );

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);

    }
}

