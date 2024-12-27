using AutoMapper;
using DateAppAPI.DTOs;
using DateAppAPI.Entities;
using DateAppAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace DateAppAPI.Controllers;

// Yêu cầu xác thực và ủy quyền
// [Authorize]
public class UserController(IUserRepo _userRepo):BaseAPIController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUsers(){
        var users = await _userRepo.GetAllUserAsync();
        return Ok(users);
    }

    [HttpGet("user-name/{userName}")]
    public async Task<ActionResult<MemberDTO>> GetUser([FromRoute]string userName){
        // var user = _dataContext.AppUsers?.Where(u => u.Id == userId).FirstOrDefault();
        var user = await _userRepo.GetUserByUserNameAsync(userName);
        if(user == null) return NotFound();
        return Ok(user);
    }

    [HttpGet("user-id/{id}")]
    public async Task<ActionResult> GetUserById([FromRoute]int id){
        var user = await _userRepo.GetUserByIdAsync(id);
        return Ok(user);
    }
}