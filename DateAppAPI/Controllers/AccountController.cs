using System.Security.Cryptography;
using System.Text;
using DateAppAPI.Data;
using DateAppAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using DateAppAPI.DTOs;
using DateAppAPI.Interfaces;
using DateAppAPI.Errors;

namespace DateAppAPI.Controllers;

public class AccountController(IUserRepo _userRepo, ITokenService _tokenService):BaseAPIController{
    // [HttpPost("register")] //account/register
    [HttpPost]
    [Route("register")]
    public async Task<ActionResult> RegisterUser([FromBody]RegisterUserDTO registerUserDTO){
        if(await _userRepo.RegisterUser(registerUserDTO)){
            // Tạo ra phản hồi StatusCodes.Status201Created
            return CreatedAtAction(nameof(RegisterUser), new { username = registerUserDTO.UserName }, registerUserDTO);
        };
            
        // Nếu người dùng đã tồn tại, trả về mã trạng thái 409 Conflict
        return Conflict("User with the same username already exists.");
    }

    [HttpDelete]
    [Route("delete/{id}")]
    public async Task<ActionResult> DeleteUserById(int id){
        if(await _userRepo.DeleteUserAsync(id)){
            return NoContent();
        }
        return NotFound();
    }

    // [HttpPut]
    // [Route("update/{id}")]
    // public async Task<ActionResult> UpdateUser(int id, [FromBody]UpdateUserDTO updateUserDTO){
    //     if(await _userRepo.UpdateUserAsync(id, updateUserDTO)){
    //         return Ok(new ApiExceptions(sttCode:200,errMsg:"Update successfuly",details:string.Empty));
    //     }

    //     return NotFound(new ApiExceptions(sttCode:400,errMsg:"User not found or update failed",details:string.Empty));
    // }

    [HttpPut]
    [Route("update/{id}")]
    public async Task<ActionResult> UpdateUser(int id, [FromBody]UpdateUserDTO updateUserDTO){
        var result = await _userRepo.UpdateUserAsync(id, updateUserDTO);
        if(result.SttCode == 404) return NotFound();
        if(result.SttCode == 409) return Conflict($"{result.ErrMSg}");
        if(result.SttCode == 1) return StatusCode(500, new ApiExceptions(sttCode: 500, errMsg: result.ErrMSg, details: result.Details));
        
        return Ok();
    }

    // login
    // [HttpPost]
    // [Route("login")]
    // public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO){
    //     var user = await _conte.AppUsers.Where(u => u.UserName == loginDTO.UserName)
    //                                     .FirstOrDefaultAsync();
    //     if(user == null) return Unauthorized("User name is invalid.");

    //     using var hmac = new HMACSHA512(user.PwdSalt);
    //     var pwdHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Pwd));
    //     // for(int i = 0; i < pwdHash.Length; i++){
    //     //     if(pwdHash[i] != user.PwdHash[i]){
    //     //         return Unauthorized("Password is invalid.");
    //     //     }
    //     // }
    //     if(!pwdHash.SequenceEqual(user.PwdHash)) return Unauthorized("Password is invalid.");
    //     return new UserDTO{
    //         UserName = user.UserName,
    //         Token = _tokenService.CreateToken(user),
    //     };;
    // }
}
