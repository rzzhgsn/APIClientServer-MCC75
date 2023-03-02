using API.Base;
using API.Contexts;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UniversitiesController : BaseController<int, University, UniversityRepository>
{
	public UniversitiesController(UniversityRepository repository) : base(repository)
	{

	}
}
