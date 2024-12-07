using WebAPI.DTOs.Stock;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.Interfaces;

public interface IStockRepository {
    Task<List<Stock>> GetAllAsync(QueryObject queryObject);
    Task<Stock?> GetByIdAsync(int id);
    Task<Stock?> GetBySymbolAsync(string symbol);
    Task<Stock?> CreateNewStockAsync(Stock? newStock);
    Task<Stock?> UpdateStockByIdAsync(int id,UpdateStockRequestDTO updateStock);
    Task<Stock?> DeleteStockAsync(int id);
    Task<bool> IsStockExists(int id);
    
}