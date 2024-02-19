using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JobPortal.DTO;
using JobPortal.Models;
using JobPortal.Repository.IRepostiory;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JobPortal.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AccountController : Controller
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _configuration;

    public AccountController(IUserRepository userRepository, UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;

    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterationRequestDTO model)
    {
        var registerResponse = await _userRepository.Register(model);
       
        return Ok(registerResponse);       
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
    {
        var loginResponse = await _userRepository.SignIn(model);
       
        return Ok(loginResponse);
    }
    [HttpGet("Confirm")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
    {
        var result = await _userRepository.Confirm(userId, token);
        if (!result.Succeeded) {
            return BadRequest(result);
        }

        return Ok(result);
    }
   

  
}