using System.ComponentModel.DataAnnotations;

namespace Login.Models.Dtos.Authentication;

public class AuthDto
{
    [Required]
    public string? Username { get; set; }

    [Required]
    public string? Code { get; set; }
}
