using Microsoft.AspNetCore.Mvc;
using Mod4.Lection4.Hw1.Application.DTOs;
using Mod4.Lection4.Hw1.Domain.Interfaces;
using Mod4.Lection4.Hw1.Domain.Models;


namespace Mod4.Lection4.Hw1.Application.Controllers;

[Route("api/showcase")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IManagerRepository _managerRepository;
    public UserController(IManagerRepository managerRepository) => _managerRepository = managerRepository;


    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var users = await _managerRepository.UserRepository.GetAllUsersAsync();

        users.OrderByDescending(u => u.Username);
        
        return Ok(users);
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetByIdAsync(Guid userId)
    {
        var user = await _managerRepository.UserRepository.GetUserAsync(userId);
        
        if (user == null) return NotFound();
        
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] UserDto userDto)
    {
        var userId = Guid.NewGuid();
        
        var user = new User
        {
            Id = userId,
            Username = userDto.Username,
        };
        
        await _managerRepository.UserRepository.CreateUserAsync(user);
        await _managerRepository.SaveChangesRepository.SaveChangesAsync();
        
        return StatusCode(201);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ChangeAsync([FromRoute] Guid id, [FromBody] UserDto userDto)
    {
        var user = await _managerRepository.UserRepository.GetUserAsync(id);
        
        if (user == null) return NotFound();

        await _managerRepository.UserRepository.UpdateUserAsync(user);
        await _managerRepository.SaveChangesRepository.SaveChangesAsync();

        return Ok(user);
    }

    [HttpDelete("id")]
    public async Task<IActionResult> DeleteAsync(Guid userId)
    {
        var user = await _managerRepository.UserRepository.GetUserAsync(userId);

        if (user == null) return NotFound();

        await _managerRepository.UserRepository.DeleteUserAsync(user);

        return StatusCode(204);
    }
    
    // 1-* 
    [HttpPut("{id}/order")]
    public async Task<IActionResult> AddOrdersAsync([FromRoute] Guid id, ICollection<Order> orders)
    {
        await _managerRepository.UserRepository.AddOrders(id, orders);
        await _managerRepository.SaveChangesRepository.SaveChangesAsync();

        return Ok(orders);
    }
}
