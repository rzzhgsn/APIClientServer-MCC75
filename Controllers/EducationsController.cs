using API.Base;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using API.Repositories.Data;
using API.Repositories.Interface;
using MCC75_MVC.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EducationsController : BaseController<int, Education, EducationRepository>
{
	public EducationsController(EducationRepository repository) : base(repository)
	{
			
	}
}