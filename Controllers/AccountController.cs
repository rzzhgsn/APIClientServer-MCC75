using API.Models;
using API.Repositories.Data;
using API.Repositories.Interface;
using MCC75_MVC.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly AccountRepository accountrepository;

    public AccountController(AccountRepository accountrepository)
    {
        this.accountrepository = accountrepository;
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
            return result is false
                ? Conflict(new
                {
                    statusCode = 409,
                    message = "Account or Password  Does not Match!"
                })
                : Ok(new
                {
                    statusCode = 200,
                    essage = "Login Success!"
                });
        }
        catch
        {
            return BadRequest(new { statusCode = 400, massage = "Something Wrong!" });
        }
    }
}
