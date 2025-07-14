using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShoppingCart.Application.Dto.LoginDTOs;
using ShoppingCart.Domain.Models.Login;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace ShoppingCart.API.Controllers;

/// <summary>
/// Controller usado para LOGIN e POST de usuario
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _configuration;

    public AuthController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    /// <summary>
    /// Metodo para registrar um usuario
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistroDto model)
    {

        var user = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email,
            Nome = model.Nome
        };

        var result = await _userManager.CreateAsync(user, model.Senha);

        if (result.Succeeded)
            return Ok("Usuário registrado com sucesso.");

        return BadRequest(result.Errors);
    }

    /// <summary>
    /// Metodo para logar com um usuario ja existente
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
            return Unauthorized("E-mail inválido.");

        var result = await _signInManager.CheckPasswordSignInAsync(user, model.Senha, false);
        if (!result.Succeeded)
            return Unauthorized("Senha incorreta.");

        var token = GerarToken(user);
        return Ok(new { token });
    }

    private string GerarToken(ApplicationUser user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
