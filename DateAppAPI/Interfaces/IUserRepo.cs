using DateAppAPI.DTOs;
using DateAppAPI.Entities;

namespace DateAppAPI.Interfaces;

public interface IUserRepo
{
    void Update(AppUser appUser);
    Task<bool> RegisterUser(RegisterUserDTO registerUserDTO);
    Task<bool> SaveAllAsync();
    Task<AppUser?> GetUserByIdAsync(int id);
    Task<IEnumerable<MemberDTO>?> GetAllUserAsync();
    Task<MemberDTO?> GetUserByUserNameAsync(string userName);
}