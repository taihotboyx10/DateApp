using System;
using System.ComponentModel.DataAnnotations;

namespace DateAppAPI.Entities;

public class AppUser
{
    [Key]
    public int Id { get; set; }
    // [Required]
    public required string UserName { get; set; }
}
