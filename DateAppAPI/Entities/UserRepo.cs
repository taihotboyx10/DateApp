using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DateAppAPI.Data;
using DateAppAPI.DTOs;
using DateAppAPI.Errors;
using DateAppAPI.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace DateAppAPI.Entities;

public class UserRepo(DataContext _context, IMapper _mapper, ITokenService _tokenService) : IUserRepo
{

    public async Task<MemberDTO?> GetUserByUserNameAsync(string userName)
    {
        // var user = await _context.AppUsers.Where(u => u.UserName == userName)
        //                               .Include(u => u.Photos)
        //                               .SingleOrDefaultAsync();
        // return _mapper.Map<MemberDTO>(user);
        var user = await _context.AppUsers
                        .Where(u => u.UserName == userName)
                        .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
                        .SingleOrDefaultAsync();
        return user;
    }

    public async Task<IEnumerable<MemberDTO>?> GetAllUserAsync()
    {
        // return await _context.AppUsers
        //             .Include(u => u.Photos)
        //             .ToListAsync();
        return await _context.AppUsers
                    .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
                    .ToListAsync();
    }

    public async Task<AppUser?> GetUserByIdAsync(int id)
    {
        return await _context.AppUsers.FindAsync(id);
    }
    
    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<ApiExceptions> UpdateUserAsync(int id, UpdateUserDTO updateUserDTO)
    {
        // thông báo cho EF appUser đã được sửa đổi và cần được cập nhật trong DB
        // _context.Entry(appUser).State = EntityState.Modified;
        var updateUser = await _context.AppUsers.FindAsync(id);
        if(updateUser == null){
            return new ApiExceptions(404,"User is not found","");
        } 
        // User name is the same as another user
        if(await ChkExistUser(updateUserDTO.UserName, id)){
            return new ApiExceptions(409,"User name is the same as another user","");
        }

        try
        {
            // Update field
            updateUser.UserName = updateUserDTO.UserName;
            updateUser.DoB = updateUserDTO.DoB;
            updateUser.Gender = updateUserDTO.Gender;
            updateUser.Introductions = updateUserDTO.Introductions;
            updateUser.Interest = updateUserDTO.Interest;
            updateUser.LookingFor = updateUserDTO.LookingFor;
            updateUser.City = updateUserDTO.City;
            updateUser.Country = updateUserDTO.Country;
            updateUser.UpdateDate = DateTime.Now;

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return new ApiExceptions(1,ex.Message,"");
        }

        return new ApiExceptions(200,"","");
    }

    public async Task<bool> RegisterUser(RegisterUserDTO registerUserDTO)
    {
        if(await ChkExistUser(registerUserDTO.UserName, null)) return false;
        
        using var hmac = new HMACSHA512();
        var user = new AppUser(){
            UserName = registerUserDTO.UserName.ToLower(),
            PwdHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerUserDTO.Pwd)),
            PwdSalt = hmac.Key,
            DoB = registerUserDTO.DoB,
            CreateDate = registerUserDTO.CreateDate,
            Gender = registerUserDTO.Gender,
            Introductions = registerUserDTO.Introductions,
            Interest = registerUserDTO.Interest,
            LookingFor = registerUserDTO.LookingFor,
            City = registerUserDTO.City,
            Country = registerUserDTO.Country,
        };

        _context.AppUsers.Add(user);
        await _context.SaveChangesAsync();
        return true;
    }

    private async Task<bool> ChkExistUser(string newUserName, int? id)
    {
        // Chỉ kiểm tra các user khác với user hiện tại
        return await _context.AppUsers.AnyAsync(u => 
            u.Id != id && u.UserName.ToLower() == newUserName.ToLower());
    }

    public async Task<bool> Login(LoginDTO loginDTO)
    {
        var user = await _context.AppUsers.Where(u => u.UserName == loginDTO.UserName)
                                        .FirstOrDefaultAsync();
        if(user == null) return false;

        using var hmac = new HMACSHA512(user.PwdSalt);
        var pwdHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Pwd));
        if(!pwdHash.SequenceEqual(user.PwdHash)) return false;
        return true;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await _context.AppUsers.FindAsync(id);
        if(user == null){
            return false;
        }

        _context.AppUsers.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }
}