using System.Runtime.CompilerServices;
using DateAppAPI.Data;
using DateAppAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace DateAppAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController:ControllerBase
{
    private readonly DataContext _dataContext;
    public UserController(DataContext dataContext){
        this._dataContext = dataContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers(){
        var users = await _dataContext.AppUsers.ToListAsync();
        return users;
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<AppUser>> GetUser(int userId){
        // var user = _dataContext.AppUsers?.Where(u => u.Id == userId).FirstOrDefault();
        var user = await _dataContext.AppUsers.FindAsync(userId);
        if(user == null) return NotFound();
        return user;
    }
}