using API.Contexts;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UniversitiesController : ControllerBase
{
    private readonly UniversityRepository repository;

    public UniversitiesController(UniversityRepository repository)
    {
        this.repository = repository;
    }

    [HttpPost]
    public async Task<ActionResult> Insert(University entity)
    { 
        try
        {
            var result = await repository.Insert(entity);
            if (result == 0)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Data Fail to Insert!"
                });
            }
            else
            {
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Data Saved Succesfully!"
                });
            }
        }
        catch
        {
            return BadRequest(new
            {
                StatusCode = 400,
                Message = "Something Wrong!",
            });
        }
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var results = await repository.GetAll();
        if (results is null)
        {
            return Ok(new
            {
                StatusCode = 200,
                Message = "Data Kosong!",
                Data = results
            });
        }
        else
        {
            return Ok(new
            {
                StatusCode = 200,
                Message = "Data Ada!",
                Data = results
            });
        }
    }


    [HttpPut]
    public async Task<ActionResult> Update(University entity)
    {
        try
        {
            var result = await repository.Update(entity);
            if (result == 0)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Data Fail to Insert!"
                });
            }
            else
            {
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Data Saved Succesfully!"
                });
            }
        }
        catch
        {
            return BadRequest(new
            {
                StatusCode = 400,
                Message = "Something Wrong!",
            });
        }
    }

    [HttpDelete]
    public async Task<ActionResult> Delete(int key)
    {
        try
        {
            var result = await repository.Delete(key);
            if (result == 0)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Data Fail to Delete!"
                });
            }
            else
            {
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Data Deleted Succesfully!"
                });
            }
        }
        catch
        {
            return BadRequest(new
            {
                StatusCode = 400,
                Message = "Something Wrong!",
            });
        }
    }


    [HttpGet]
    [Route("{key}")]
    public async Task<ActionResult> GetById(int key)
    {
        var results = await repository.GetById(key);
        if (results is null)
        {
            return Ok(new
            {
                StatusCode = 200,
                Message = "Data Kosong!",
                Data = results
            });
        }
        else
        {
            return Ok(new
            {
                StatusCode = 200,
                Message = "Data Ada!",
                Data = results
            });
        }
    }
}
