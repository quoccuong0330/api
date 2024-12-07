using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models;

[Table("Portfolio")]
public class Portfolio {
    public string userId { get; set; }
    public int stockId { get; set; }
    public Stock Stock { get; set; }
    public User User { get; set; }
}