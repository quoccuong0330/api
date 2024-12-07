using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs.Account;

public class LoginAccountDTO {
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
    
}