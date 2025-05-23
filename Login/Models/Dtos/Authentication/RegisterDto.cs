﻿using System.ComponentModel.DataAnnotations;

namespace Login.Models.Dtos.Authentication;

public class RegisterDto
{
    [Required]
    public required string Email { get; set; }
    [Required]
    public required string Username { get; set; }
    [Required]
    public required string Password { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
}
