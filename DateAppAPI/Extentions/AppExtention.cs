using DateAppAPI.Data;
using DateAppAPI.Interfaces;
using DateAppAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace DateAppAPI.Extentions;

public static class AppExtention{
    public static IServiceCollection AddAppExtention(this IServiceCollection service, IConfiguration config){
        service.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        service.AddOpenApi();
        // Add DbContext
        service.AddDbContext<DataContext>(options => {
            options.UseSqlServer(config.GetConnectionString("DefaultConnectionStr"));
        });
        // Add token service
        service.AddScoped<ITokenService, TokenService>();
        // Add cors
        service.AddCors();

        return service;
    }
}