using System.Runtime.CompilerServices;
using DateAppAPI.Data;
using DateAppAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace DateAppAPI.Controllers;

public class UserController:BaseAPIController
{
    private readonly DataContext _dataContext;
    public UserController(DataContext dataContext){
        this._dataContext = dataContext;
    }

    // truy cập vào method or endpoint, ngay cả khi toàn bộ controller hoặc ứng dụng yêu cầu xác thực.
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers(){
        var users = await _dataContext.AppUsers.ToListAsync();
        return users;
    }

    // Yêu cầu xác thực và ủy quyền
    [Authorize]
    [HttpGet("{userId}")]
    public async Task<ActionResult<AppUser>> GetUser(int userId){
        // var user = _dataContext.AppUsers?.Where(u => u.Id == userId).FirstOrDefault();
        var user = await _dataContext.AppUsers.FindAsync(userId);
        if(user == null) return NotFound();
        return user;
    }
}