using DateAppAPI.DTOs;
using DateAppAPI.Entities;
using DateAppAPI.Errors;

namespace DateAppAPI.Interfaces;

public interface IUserRepo
{
    Task<bool> RegisterUser(RegisterUserDTO registerUserDTO);
    Task<bool> Login(LoginDTO loginDTO);
    Task<bool> SaveAllAsync();
    Task<AppUser?> GetUserByIdAsync(int id);
    Task<IEnumerable<MemberDTO>?> GetAllUserAsync();
    Task<MemberDTO?> GetUserByUserNameAsync(string userName);
    Task<bool> DeleteUserAsync(int id);
    Task<ApiExceptions> UpdateUserAsync(int id, UpdateUserDTO updateUserDTO);
}