using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Interfaces;
using WebAPI.Mappers;
using WebAPI.Models;

namespace WebAPI.Repositories;

public class PortfolioRepository : IPortfolioRepository {
    private readonly ApplicationDBContext _context;
    public PortfolioRepository(ApplicationDBContext context) {
        _context = context;
    }
    public  Task<List<Stock>> GetUserPortfolio(User user) {
        return _context.Portfolios.Where(u => u.userId == user.Id).Select(stock => new Stock {
            Id = stock.stockId,
            Symbol = stock.Stock.Symbol,
            CompanyName = stock.Stock.CompanyName,
            LastDiv = stock.Stock.LastDiv,
            Industry = stock.Stock.Industry,
            MarketCap = stock.Stock.MarketCap,
            Purchase = stock.Stock.Purchase
        }).ToListAsync();
    }

    public async Task<Portfolio> AddStockToPortfolio(Portfolio portfolio) {
         await _context.Portfolios.AddAsync(portfolio);
         await _context.SaveChangesAsync();
         return portfolio;
    }

    public async Task<Portfolio> DeleteStockFromPortfolio(User user, string symbol) {
        var portfolioModel = await _context.Portfolios
            .FirstOrDefaultAsync(x => x.userId == user.Id && x.Stock.Symbol.ToLower() == symbol.ToLower());
        if (portfolioModel == null) return null;
         _context.Portfolios.Remove(portfolioModel);
         await _context.SaveChangesAsync();
         return portfolioModel;
    }
}