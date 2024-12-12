using DateAppAPI.Entities;

namespace DateAppAPI.DTOs;

public class MemberDTO{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public int Age { get; set; }
    public string? PhotoURL { get; set; }
    // public DateTime CreateDate { get; set; }
    public DateTime LastActive { get; set; }
    public string? Gender { get; set; }
    public string? Introductions { get; set; }
    // public string? Interest { get; set; }
    // public string? LookingFor { get; set; }
    // public string? City { get; set; }
    // public string? Country { get; set; }
    // public List<PhotoDTO>? Photos { get; set; }
    public string? Address { get; set; }
}