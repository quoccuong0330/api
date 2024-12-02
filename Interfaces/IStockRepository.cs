using WebAPI.DTOs.Stock;
using WebAPI.Models;

namespace WebAPI.Interfaces;

public interface IStockRepository {
    Task<List<Stock>> GetAllAsync();
    Task<Stock?> GetByIdAsync(int id);
    Task<Stock> CreateNewStockAsync(Stock newStock);
    Task<Stock?> UpdateStockByIdAsync(int id,UpdateStockRequestDTO updateStock);
    Task<Stock?> DeleteStockAsync(int id);
}