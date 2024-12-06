using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data;

public class ApplicationDBContext : IdentityDbContext<User> {
    public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions){
        
    }
    
    public DbSet<Stock> Stock { get; set; }
    public DbSet<Comment?> Comments { get; set; }
    public DbSet<Portfolio> Portfolios { get; set; }
    
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Portfolio>(x=> x.HasKey(p=> new  {
            p.stockId, p.userId
        }));
        modelBuilder.Entity<Portfolio>().HasOne(u => u.User)
            .WithMany(u => u.Portfolios)
            .HasForeignKey(p => p.userId);
        
        modelBuilder.Entity<Portfolio>().HasOne(u => u.Stock)
            .WithMany(u=>u.Portfolios)
            .HasForeignKey(p => p.stockId);
        
        

        List<IdentityRole> roles = new List<IdentityRole>() {
            new IdentityRole {
                Name = "Alice",
                NormalizedName = "ADMIN"
            },
            new IdentityRole {
                Name = "Rai",
                NormalizedName = "USER"
            }
        };
        modelBuilder.Entity<IdentityRole>().HasData(roles);

        // Chèn dữ liệu mẫu cho bảng Stocks
        // modelBuilder.Entity<Stock>().HasData(
        //     new Stock
        //     {
        //         Id = 1,
        //         Symbol = "AAPL",
        //         CompanyName = "Apple Inc.",
        //         Purchase = 150.00M,
        //         LastDiv = 1.23M,
        //         Industry = "Technology",
        //         MarketCap = 2500000000000,
        //     },
        //     new Stock
        //     {
        //         Id = 2,
        //         Symbol = "MSFT",
        //         CompanyName = "Microsoft Corp.",
        //         Purchase = 200.00M,
        //         LastDiv = 1.45M,
        //         Industry = "Technology",
        //         MarketCap = 2200000000000,
        //     }
        // );
    }
}