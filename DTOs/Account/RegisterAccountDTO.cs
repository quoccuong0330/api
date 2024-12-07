using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs.Account;

public class RegisterAccountDTO {
    [Required]
    public string? Username { get; set; }
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
    
}