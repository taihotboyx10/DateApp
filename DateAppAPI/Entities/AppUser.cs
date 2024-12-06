using System;
using System.ComponentModel.DataAnnotations;

namespace DateAppAPI.Entities;

public class AppUser
{
    [Key]
    public int Id { get; set; }
    // [Required]
    public required string UserName { get; set; }
    public required byte[] PwdHash { get; set; }
    public required byte[] PwdSalt { get; set; }
}
