using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DateAppAPI.Data;
using DateAppAPI.DTOs;
using DateAppAPI.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace DateAppAPI.Entities;

public class UserRepo(DataContext _context, IMapper _mapper) : IUserRepo
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

    public void Update(AppUser appUser)
    {
        // thông báo cho EF appUser đã được sửa đổi và cần được cập nhật trong DB
        _context.Entry(appUser).State = EntityState.Modified;
    }

    public async Task<bool> RegisterUser(RegisterUserDTO registerUserDTO)
    {
        if(await ChkExistUser(registerUserDTO.UserName)) return false;
        
        using var hmac = new HMACSHA512();
        var user = new AppUser(){
            UserName = registerUserDTO.UserName.ToLower(),
            PwdHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerUserDTO.Pwd)),
            PwdSalt = hmac.Key,
        };

        _context.AppUsers.Add(user);
        await _context.SaveChangesAsync();
        return true;
    }

    private async Task<bool> ChkExistUser(string newUserName){
        var result = await _context.AppUsers.AnyAsync(u => u.UserName.ToLower() == newUserName.ToLower());
        return result;
    }
}