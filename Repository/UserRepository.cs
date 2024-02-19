using JobPortal.DTO;
using JobPortal.DTO.ReturnObjects;
using JobPortal.Models;
using JobPortal.Repository.IRepostiory;
using JobPortal.Utility;
using JobPortal.Utility.Email;

namespace JobPortal.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
public class UserRepository:IUserRepository
{
    private readonly EmailSettings? _emailSettings;

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailSender _emailSender;

    private readonly string? _secretKey;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public UserRepository(IConfiguration configuration, SignInManager<ApplicationUser> signInManage,
        UserManager<ApplicationUser> userManager,IEmailSender emailSender)
    {
        _userManager = userManager;
        _signInManager = signInManage;
        _emailSender = emailSender;
        _emailSettings = configuration.GetSection("EmailSettings").Get<EmailSettings>();

        _secretKey = configuration.GetValue<string>("ApiSettings:Secret");
    }

    public async    Task<Response<RegisterResponse>> Register(RegisterationRequestDTO signUp)
    {
        try
        {
            var findUser = await _userManager.FindByNameAsync(signUp.UserName);
            if(findUser!=null)return ResponseFactory.Fail<RegisterResponse>();
            var user = new ApplicationUser
            {
                UserName = signUp.UserName,
                Email = signUp.Email,
                FirstName = signUp.FirstName,
                LastName = signUp.LastName
            };

            var result = await _userManager.CreateAsync(user, signUp.Password);
            if (result.Succeeded == false)return ResponseFactory.Fail<RegisterResponse>();
            var sendEmailResult = await SendEmailConfirmationTokenAsync(user.Id);

            var data = new RegisterResponse
            {
                Email = signUp.Email,
                Username = signUp.UserName
            };
            return !sendEmailResult.Succeeded
                ? ResponseFactory.Fail(sendEmailResult.Errors!, data)
                : ResponseFactory.Ok(data);

        }
        catch (Exception ex)
        {
            return ResponseFactory.FailFromException<RegisterResponse>(ex);
        }
    }

    public async Task<Response<LoginResponse>> SignIn(LoginRequestDTO loginRequestDto)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDto.UserName);

            if (user == null) return ResponseFactory.Fail<LoginResponse>(ErrorsList.CannotFindUser);

            var result = await _signInManager.PasswordSignInAsync(user, loginRequestDto.Password, false, false);

            if (!result.Succeeded) return ResponseFactory.Fail<LoginResponse>(ErrorsList.IncorrectEmailOrPassword);

            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var rolesClaims = roles.Select(role => new Claim("roles", role)).ToList();

            var authClaims = new List<Claim>
                {
                    new(ClaimTypes.Name, loginRequestDto.UserName),
                    new(ClaimTypes.NameIdentifier, user.Id),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }.Union(userClaims)
                .Union(rolesClaims);
            var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secretKey));

            var token = new JwtSecurityToken(
                issuer: "your_issuer", 
                audience: "your_audience",
                expires: DateTime.Now.AddMinutes(10),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)
            );
            var data = new LoginResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                User = new UserDTO()
                {
                    UserName = user.UserName,
                    ID = user.Id,
                    Name = user.FirstName
                }
            };
            return ResponseFactory.Ok(data);
        }
        catch (Exception e)
        {
            return ResponseFactory.FailFromException<LoginResponse>(e);

        }
        
    }

    private async Task<Response> SendEmailConfirmationTokenAsync(string userId)
    {
        
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return ResponseFactory.Fail(ErrorsList.CannotFindUser);
        }

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        token = System.Web.HttpUtility.UrlEncode(token);

        if (_emailSettings?.ConfirmationLinkBase == null) return ResponseFactory.Fail(ErrorsList.CannotFindUser);
        var confirmationLink = new UriBuilder(_emailSettings?.ConfirmationLinkBase)
        {
            Path = "api/Account/Confirm",
            Query = $"id={user.Id}&token={token}"
        };

        const string subject = "Competitive Programming Email Confirmation";
        var message = $"Hello {user.FirstName}<br>";
        message += $"This is your confirmation <a href=\"{confirmationLink}\">Link</a>";

        return user.Email != null ? _emailSender.SendEmail(user.Email, subject, message) : ResponseFactory.Fail(ErrorsList.CannotFindUser);
    }
    public async Task<Response> Confirm(string id, string token)
    {
        
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            return ResponseFactory.Fail(ErrorsList.CannotFindUser);
        
        var confirmResult = await _userManager.ConfirmEmailAsync(user, token);
        return confirmResult.Succeeded? ResponseFactory.Ok():ResponseFactory.Fail();
    }
}