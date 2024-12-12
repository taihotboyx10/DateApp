using System.ComponentModel.DataAnnotations;

namespace DateAppAPI.DTOs;

public class UpdateUserDTO{
    [Required]
    [MaxLength(20, ErrorMessage ="User name must be at most 20 characters")]
    public string UserName { get; set; } = string.Empty;
    public DateOnly DoB { get; set; }
    public string Gender { get; set; } = string.Empty;
    [MaxLength(20)]
    public string? Introductions { get; set; }
    public string? Interest { get; set; }
    public string? LookingFor { get; set; }

    [MaxLength(15, ErrorMessage ="City must be at most 15 characters")]
    public string? City { get; set; }

    [MaxLength(15, ErrorMessage ="Country must be at most 15 characters")]
    public string? Country { get; set; }
}