using Interview_Test.Api.DTOs;
using Interview_Test.Models;
using Interview_Test.Repositories;
using Interview_Test.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Interview_Test.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet("GetUserById/{id}")]
    public ActionResult<UserResponse> GetUserById(string id)
    {
        var data = _userRepository.GetUserById(id);
        
        if (data == null)
        {
            return NotFound();
        }
        
        return Ok(data.Users);
    }
    
    [HttpPost("CreateUser")]
    public ActionResult CreateUser([FromBody] UserModel user)
    {
        var rowsAffected = _userRepository.CreateUser(user);
        
        if (rowsAffected > 0)
        {
            return Ok(new { message = "User created successfully", rowsAffected });
        }
        
        return BadRequest(new { message = "Failed to create user" });
    }
}