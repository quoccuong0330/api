using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTOs.Stock;
using WebAPI.Mappers;
using WebAPI.Models;

namespace WebAPI.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController : ControllerBase {
    private readonly ApplicationDBContext _context;
    public StockController(ApplicationDBContext dbContext) {
        _context = dbContext;
    }

    [HttpGet]
    public IActionResult GetAll() {
        var stocks = _context.Stock.ToList().Select(s => s.ToStockDto());
        return Ok(stocks);
    }

    [HttpGet("{id}")]
    public IActionResult GetStockById([FromRoute] int id) {
        var stockById = _context.Stock.Find(id);
        return stockById != null ? Ok(stockById.ToStockDto()) : NotFound();
    }

    [HttpPost]
    public IActionResult CreateNewStock([FromBody] CreateStockRequestDTO createStock) {
        var stockModel = createStock.ToStockFromCreateDTO();
        _context.Add(stockModel);
        
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetStockById), new {id = stockModel.Id}, stockModel.ToStockDto());
    }

    [HttpPut("{id}")]
    public IActionResult UpdateStockById([FromRoute] int id, [FromBody] UpdateStockRequestDTO updateStock) {
        var stockModel = _context.Stock.FirstOrDefault(x => x.Id == id);
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
    public IActionResult DeleteStockById([FromRoute] int id) {
        var stockModel = _context.Stock.FirstOrDefault(x=> x.Id == id);
        if (stockModel == null) return NotFound();
        
        _context.Stock.Remove(stockModel);
        _context.SaveChanges();
        return NoContent();
    }
    
}