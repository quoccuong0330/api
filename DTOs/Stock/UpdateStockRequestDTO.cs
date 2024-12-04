using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.DTOs.Stock;

public class UpdateStockRequestDTO {
    [Required]
    [MinLength(3, ErrorMessage = "Symbol must be 3 characters")]
    [MaxLength(4, ErrorMessage = "Symbol cannot over 4 characters")]
    public string Symbol { get; set; } = string.Empty;
    [Required]
    [MinLength(10, ErrorMessage = "Company name must be 3 characters")]
    public string CompanyName { get; set; } = string.Empty;
    [Column(TypeName= "decimal(18,2)")]
    [Required]
    [Range(1,1000000000)]
    public decimal Purchase { get; set; }
    [Column(TypeName= "decimal(18,2)")]
    [Required]
    [Range(0.001,100)]
    public decimal LastDiv { get; set; }
    [Required]
    [MinLength(10,ErrorMessage = "Industry must be 10 characters")]
    public string Industry { get; set; } = string.Empty;
    [Required]
    [Range(1,5000000000)]
    public long MarketCap { get; set; }

   
}