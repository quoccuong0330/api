using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTOs.Stock;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repositories;

public class StockRepository : IStockRepository {
    private readonly ApplicationDBContext _context;
    
    public StockRepository(ApplicationDBContext context) {
        _context = context;
    }
    
    public async Task<List<Stock>> GetAllAsync() {
        return await _context.Stock.ToListAsync();
    }

    public async Task<Stock?> GetByIdAsync(int id) {
        return await _context.Stock.FirstOrDefaultAsync(x=>x.Id == id);
    }

    public async Task<Stock> CreateNewStockAsync(Stock newStock) {
         await _context.Stock.AddAsync(newStock);
         await _context.SaveChangesAsync();
         return newStock;
    }

    public async Task<Stock?> UpdateStockByIdAsync(int id, UpdateStockRequestDTO updateStock) {
        var stockModel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
        if (stockModel == null) return null;
        stockModel.Industry = updateStock.Industry;
        stockModel.MarketCap = updateStock.MarketCap;
        stockModel.Symbol = updateStock.Symbol;
        stockModel.CompanyName = updateStock.CompanyName;
        stockModel.Purchase = updateStock.Purchase;
        stockModel.LastDiv = updateStock.LastDiv;
        await _context.SaveChangesAsync();
        return stockModel;

    }
    

    public async Task<Stock?> DeleteStockAsync(int id) {
        var stockModel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
        if (stockModel == null) return null;
         _context.Stock.Remove(stockModel);
         await _context.SaveChangesAsync();
         return stockModel;
    }
}