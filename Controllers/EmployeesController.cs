using API.Base;
using API.Models;
using API.Repositories.Data;
using API.Repositories.Interface;
using MCC75_MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : BaseController<string, Employee, EmployeeRepository>
{
	public EmployeesController(EmployeeRepository repository) : base(repository)
	{

	}
}