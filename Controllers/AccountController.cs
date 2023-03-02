using API.Models;
using API.Repositories.Data;
using API.Repositories.Interface;
using MCC75_MVC.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly AccountRepository accountrepository;
    private readonly IConfiguration configuration;

    public AccountController(AccountRepository accountrepository, IConfiguration configuration)
    {
        this.accountrepository = accountrepository;
        this.configuration = configuration;
    }

    [HttpPost("Register")]
    public async Task<ActionResult> Register(RegisterVM registerVM)
    {
        try
        {
            var result = await accountrepository.Register(registerVM);
            return result is 0
                ? Conflict(new { statusCode = 409, message = "Data fail to Insert!" })
                : Ok(new { statusCode = 200, message = "Data Saved Succesfully!" });
        }
        catch
        {
            return BadRequest(new { statusCode = 400, message = "Something Wrong!" });
        }
    }

    [HttpPost("Login")]
    public async Task<ActionResult> Login(LoginVM loginVM)
    {
        try
        {
            var result = await accountrepository.Login(loginVM);
            if ( result is false)
                {
                return Conflict(new
                {
                    statusCode = 409,
                    message = "Account or Password  Does not Match!"
                });
            }
            else
            {
                var userdata = accountrepository.GetUserdata(loginVM.Email);
                var roles = accountrepository.GetRolesByNIK(loginVM.Email);

                var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, userdata.Email),
                new Claim(ClaimTypes.Name, userdata.FullName)
            };

                foreach (var item in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item));
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: configuration["JWT:Issuer"],
                    audience: configuration["JWT:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: signIn
                    );

                var generateToken = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new { statusCode = 200, message = "Login Success!", data = generateToken });
            }
        }
        catch
        {
            return BadRequest(new { statusCode = 400, massage = "Something Wrong!" });
        }
    }
}
