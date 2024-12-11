using System.ComponentModel.DataAnnotations;

namespace DateAppAPI.DTOs;

public class RegisterUserDTO{
    [Required]
    [MaxLength(20)]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    [MaxLength(15)]
    public string Pwd { get; set; } = string.Empty;
}