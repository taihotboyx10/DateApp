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
        .ForMember(u => u.PhotoURL, opt => opt.MapFrom(p => p.Photos.Where(p => p.IsMain).FirstOrDefault()!.Url));
        CreateMap<Photo, PhotoDTO>();
    }
}