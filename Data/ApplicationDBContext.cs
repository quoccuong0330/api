using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data;

public class ApplicationDBContext : DbContext{
    public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions){
        
    }
    
    public DbSet<Stock> Stock { get; set; }
    public DbSet<Comment> Comments { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Chèn dữ liệu mẫu cho bảng Stocks
        modelBuilder.Entity<Stock>().HasData(
            new Stock
            {
                Id = 1,
                Symbol = "AAPL",
                CompanyName = "Apple Inc.",
                Purchase = 150.00M,
                LastDiv = 1.23M,
                Industry = "Technology",
                MarketCap = 2500000000000,
            },
            new Stock
            {
                Id = 2,
                Symbol = "MSFT",
                CompanyName = "Microsoft Corp.",
                Purchase = 200.00M,
                LastDiv = 1.45M,
                Industry = "Technology",
                MarketCap = 2200000000000,
            }
        );
    }
}