using System.ComponentModel.DataAnnotations;

namespace DateAppAPI.DTOs;

public class RegisterUserDTO{
    [Required]
    [MaxLength(20)]
    public required string UserName { get; set; }

    [Required]
    [MinLength(6)]
    [MaxLength(15)]
    public required string Pwd { get; set; }
}