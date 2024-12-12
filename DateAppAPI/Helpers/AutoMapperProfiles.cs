using AutoMapper;
using DateAppAPI.DTOs;
using DateAppAPI.Entities;
using DateAppAPI.Extentions;

namespace DateAppAPI.Helpers;

public class AutoMapperProfiles : Profile{
    public AutoMapperProfiles(){
        CreateMap<AppUser, MemberDTO>()
        .ForMember(u => u.Age, opt => opt.MapFrom(p => p.DoB.CalcAge()))
        // ! -> đảm bảo giá trị trả về sẽ không phải là null
        .ForMember(u => u.PhotoURL, opt => opt.MapFrom(p => p.Photos.FirstOrDefault(p => p.IsMain)!.Url))
        .ForMember(u => u.Address, opt => opt.MapFrom(p => (string.IsNullOrEmpty(p.Country) || string.IsNullOrEmpty(p.City)) ? $"{p.Country}{p.City}" : $"{p.Country},{p.City}"));

        CreateMap<Photo, PhotoDTO>();

        // CreateMap<AppUser, RegisterUserDTO>();
    }
}