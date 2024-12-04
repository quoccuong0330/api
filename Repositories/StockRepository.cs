using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTOs.Stock;
using WebAPI.Helpers;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repositories;

public class StockRepository : IStockRepository {
    private readonly ApplicationDBContext _context;
    
    public StockRepository(ApplicationDBContext context) {
        _context = context;
    }
    
    public async Task<List<Stock>> GetAllAsync(QueryObject queryObject) {
        var stocks =   _context.Stock.Include(i => i.Comments).AsQueryable();
        if (!string.IsNullOrWhiteSpace(queryObject.CompanyName)) {
            stocks = stocks.Where(s => s.CompanyName.Contains(queryObject.CompanyName));
        }
        if (!string.IsNullOrWhiteSpace(queryObject.Symbol)) {
            stocks = stocks.Where(s => s.Symbol.Contains(queryObject.Symbol));
        }

        if (!string.IsNullOrWhiteSpace(queryObject.SortBy)) {
            if (queryObject.SortBy.Equals("Symbol",StringComparison.OrdinalIgnoreCase)) {
                stocks = !queryObject.IsDecreaseing
                    ? stocks.OrderByDescending(stock => stock.Symbol)
                    : stocks.OrderBy(stock => stock.Symbol);
            }
        }

        var skipNumber = (queryObject.PageSize * (queryObject.PageNumber - 1));
        
        return await stocks.Skip(skipNumber).Take(queryObject.PageNumber).ToListAsync();
    }

    public async Task<Stock?> GetByIdAsync(int id) {
        return await _context.Stock.Include(i => i.Comments).FirstOrDefaultAsync(x=>x.Id == id);
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

    public async Task<bool> isStockExists(int id) {
        return await _context.Stock.AnyAsync(x => x.Id == id);
    }
}