using WebAPI.Models;

namespace WebAPI.Interfaces;

public interface IPortfolioRepository {
    Task<List<Stock>> GetUserPortfolio(User user);
    Task<Portfolio> AddStockToPortfolio(Portfolio portfolio);
    Task<Portfolio> DeleteStockFromPortfolio(User user, string symbol);
}