using System;
using System.ComponentModel.DataAnnotations;
using DateAppAPI.Extentions;

namespace DateAppAPI.Entities;

public class AppUser
{
    [Key]
    public int Id { get; set; }
    public required string UserName { get; set; }
    public byte[] PwdHash { get; set; } = [];
    public byte[] PwdSalt { get; set; } = [];
    public DateOnly DoB { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    public DateTime LastActive { get; set; } = DateTime.UtcNow;
    public string? Gender { get; set; }
    public string? Introductions { get; set; }
    public string? Interest { get; set; }
    public string? LookingFor { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public List<Photo>? Photos { get; set; } = new ();

    // public int GetAge(){
    //     return DoB.CalcAge();
    // }
}
