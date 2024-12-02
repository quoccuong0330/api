using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTOs.Stock;
using WebAPI.Mappers;


namespace WebAPI.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController : ControllerBase {
    private readonly ApplicationDBContext _context;
    public StockController(ApplicationDBContext dbContext) {
        _context = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() {
        var stocks = await _context.Stock.ToListAsync();
        var stocksDTO = stocks.Select(s => s.ToStockDto());
        return Ok(stocksDTO);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStockById([FromRoute] int id) {
        var stockById = await _context.Stock.FindAsync(id);
        return stockById != null ? Ok(stockById.ToStockDto()) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewStock([FromBody] CreateStockRequestDTO createStock) {
        var stockModel = createStock.ToStockFromCreateDTO();
        await _context.AddAsync(stockModel);
        
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetStockById), new {id = stockModel.Id}, stockModel.ToStockDto());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStockById([FromRoute] int id, [FromBody] UpdateStockRequestDTO updateStock) {
        var stockModel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
        if (stockModel == null) {
            return NotFound();
        }

        stockModel.Industry = updateStock.Industry;
        stockModel.MarketCap = updateStock.MarketCap;
        stockModel.Symbol = updateStock.Symbol;
        stockModel.CompanyName = updateStock.CompanyName;
        stockModel.Purchase = updateStock.Purchase;
        stockModel.LastDiv = updateStock.LastDiv;

        _context.SaveChanges();
        return Ok(stockModel);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStockById([FromRoute] int id) {
        var stockModel = await _context.Stock.FirstOrDefaultAsync(x=> x.Id == id);
        if (stockModel == null) return NotFound();
        
         _context.Stock.Remove(stockModel);
        _context.SaveChanges();
        return NoContent();
    }
    
}