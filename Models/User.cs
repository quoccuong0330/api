using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace WebAPI.Models;

public class User : IdentityUser {
    public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
}