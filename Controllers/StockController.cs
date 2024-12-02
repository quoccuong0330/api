using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
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
}