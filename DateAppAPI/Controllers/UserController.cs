using AutoMapper;
using DateAppAPI.DTOs;
using DateAppAPI.Entities;
using DateAppAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace DateAppAPI.Controllers;

// Yêu cầu xác thực và ủy quyền
[Authorize]
public class UserController(IUserRepo _userRepo):BaseAPIController
{
    // truy cập vào method or endpoint, ngay cả khi toàn bộ controller hoặc ứng dụng yêu cầu xác thực.
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUsers(){
        var users = await _userRepo.GetAllUserAsync();
        return Ok(users);
    }

    [HttpGet("{userName}")]
    public async Task<ActionResult<MemberDTO>> GetUser(string userName){
        // var user = _dataContext.AppUsers?.Where(u => u.Id == userId).FirstOrDefault();
        var user = await _userRepo.GetUserByUserNameAsync(userName);
        if(user == null) return NotFound();
        return user;
    }
}