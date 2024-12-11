using DateAppAPI.Data;
using DateAppAPI.Entities;
using DateAppAPI.Interfaces;
using DateAppAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace DateAppAPI.Extentions;

public static class AppExtention{
    public static IServiceCollection AddAppExtention(this IServiceCollection service, IConfiguration config){
        service.AddControllers();
        // Add DbContext
        service.AddDbContext<DataContext>(options => {
            options.UseSqlServer(config.GetConnectionString("DefaultConnectionStr"));
        });
        // Add token service
        service.AddScoped<ITokenService, TokenService>();
        // Add cors
        service.AddCors();
        // Add user repo
        service.AddScoped<IUserRepo, UserRepo>();
        // Add auto mapper
        service.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return service;
    }
}