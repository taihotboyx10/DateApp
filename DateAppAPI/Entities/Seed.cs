using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using DateAppAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace DateAppAPI.Entities;

public class Seed{
    public static async Task SeedUsers(DataContext context){
        if(await context.AppUsers.AnyAsync()){
            return;
        }

        var userData = File.ReadAllText("Entities/GenData.json");
        var options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};
        var users = JsonSerializer.Deserialize<List<AppUser>>(userData, options);
        if(users == null) return;
        foreach (var item in users)
        {
            using var hmac = new HMACSHA512();
            item.UserName = item.UserName.ToLower();
            item.PwdHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("password"));
            item.PwdSalt = hmac.Key;

            context.AppUsers.Add(item);
        }

        await context.SaveChangesAsync();
    }
}