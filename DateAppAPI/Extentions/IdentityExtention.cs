using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace DateAppAPI.Extentions;

public static class IdentityExtention {
    public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration config){
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    var tokenKey = config["tokenKey"] ?? throw new Exception("Cannot access tokenKey form appSettings");
                    options.TokenValidationParameters = new TokenValidationParameters(){
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                     };
                 });
        return services;
    }
}