using System.Security.Cryptography;
using System.Text;
using DateAppAPI.Data;
using DateAppAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using DateAppAPI.DTOs;
using Microsoft.EntityFrameworkCore;
using DateAppAPI.Interfaces;

namespace DateAppAPI.Controllers;

public class AccountController:BaseAPIController{
    private readonly DataContext _dataContext;
    private readonly ITokenService _tokenService;
    public AccountController(DataContext dataContext, ITokenService tokenService){
        this._dataContext = dataContext;
        this._tokenService = tokenService;
    }

    // [HttpPost("register")] //account/register
    [HttpPost]
    [Route("register")]
    // [NonAction]
    public async Task<ActionResult<UserDTO>> RegisterUser(RegisterUserDTO registerUserDTO){
        // check username existing
        if(await ChkExistUser(registerUserDTO.UserName)) return BadRequest("User name is taken");

        using var hmac = new HMACSHA512();
        var user = new AppUser(){
            UserName = registerUserDTO.UserName.ToLower(),
            PwdHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerUserDTO.Pwd)),
            PwdSalt = hmac.Key,
        };

        _dataContext.AppUsers.Add(user);
        await _dataContext.SaveChangesAsync();
        return new UserDTO{
            UserName = user.UserName,
            Token = _tokenService.CreateToken(user),
        };
    }

    // check existing user
    private async Task<bool> ChkExistUser(string newUserName){
        // var existUser = await _dataContext.AppUsers.Where(u => u.UserName == newUserName).FirstOrDefaultAsync();
        // if(existUser != null) return true;
        // return false;
        var result = await _dataContext.AppUsers.AnyAsync(u => u.UserName.ToLower() == newUserName.ToLower());
        return result;
    }

    // login
    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO){
        var user = await _dataContext.AppUsers.Where(u => u.UserName == loginDTO.UserName)
                                        .FirstOrDefaultAsync();
        if(user == null) return Unauthorized("User name is invalid.");

        using var hmac = new HMACSHA512(user.PwdSalt);
        var pwdHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Pwd));
        // for(int i = 0; i < pwdHash.Length; i++){
        //     if(pwdHash[i] != user.PwdHash[i]){
        //         return Unauthorized("Password is invalid.");
        //     }
        // }
        if(!pwdHash.SequenceEqual(user.PwdHash)) return Unauthorized("Password is invalid.");
        return new UserDTO{
            UserName = user.UserName,
            Token = _tokenService.CreateToken(user),
        };;
    }
}
