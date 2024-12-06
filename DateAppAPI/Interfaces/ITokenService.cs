using DateAppAPI.Entities;

namespace DateAppAPI.Interfaces;

public interface ITokenService
{
    string CreateToken (AppUser appUser);
}