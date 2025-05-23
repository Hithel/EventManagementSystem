﻿using System.ComponentModel.DataAnnotations;

namespace Login.Models.Dtos.Authentication;

public class AddRoleDto
{
    [Required]
    public string? Username { get; set; }
    [Required]
    public string? Password { get; set; }
    [Required]
    public string? Role { get; set; }
}
