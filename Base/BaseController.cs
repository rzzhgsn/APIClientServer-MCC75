using API.Models;
using API.Repositories.Data;
using API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Base;

[Route("api/[controller]")]
[ApiController]
public class BaseController<Key, Entity, Repository> : ControllerBase
    where Entity : class
    where Repository : IRepository<Key, Entity>
{
    private readonly Repository repository;

    public BaseController(Repository repository)
    {
        this.repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var results = await repository.GetAll();
        if (results is null)
        {
            return Ok(new
            {
                StatusCode = 404,
                Message = "Data Not Found!",
                Data = results
            });
        }
        else
        {
            return Ok(new
            {
                StatusCode = 200,
                Message = "Success!",
                Data = results
            });
        }
    }

    [HttpGet]
    [Route("Key")]
    public async Task<ActionResult> GetById(Key key)
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

    [HttpPost]
    public async Task<ActionResult> Insert(Entity entity)
    {
        try
        {
            var result = await repository.Insert(entity);
            if (result == 0)
            {
                return BadRequest(new
                {
                    StatusCode = 409,
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

    [HttpPut]
    public async Task<ActionResult> Update(Entity entity)
    {
        try
        {
            var result = await repository.Update(entity);
            return result is 0
                ? Conflict(new { statusCode = 409, message = "Data Fail to Update!" })
                : Ok(new { statusCode = 200, message = "Data Updated Succesfully!" });
        }
        catch
        {
            return BadRequest(new { statusCode = 400, message = "Something Wrong!" });
        }
    }

    [HttpDelete]
    public async Task<ActionResult> Delete(Key key)
    {
        try
        {
            var result = await repository.Delete(key);
            if (result == 0)
            {
                return BadRequest(new
                {
                    StatusCode = 404,
                    Message = "Data Not Found!"
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
}
