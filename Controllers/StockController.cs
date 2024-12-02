using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTOs.Stock;
using WebAPI.Interfaces;
using WebAPI.Mappers;


namespace WebAPI.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController : ControllerBase {
    private readonly ApplicationDBContext _context;
    private readonly IStockRepository _stockRepository;

    public StockController(ApplicationDBContext dbContext,IStockRepository stockRepository) {
        _stockRepository = stockRepository;
        _context = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() {
        var stocks = await _stockRepository.GetAllAsync();
        var stocksDto = stocks.Select(s => s.ToStockDto());
        return Ok(stocksDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStockById([FromRoute] int id) {
        var stockModel = await _stockRepository.GetByIdAsync(id);
        return stockModel != null ? Ok(stockModel) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewStock([FromBody] CreateStockRequestDTO createStock) {
        var stockModel = await _stockRepository.CreateNewStockAsync(createStock.ToStockFromCreateDTO());
        return CreatedAtAction(nameof(GetStockById), new {id = stockModel.Id}, stockModel.ToStockDto());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStockById([FromRoute] int id, [FromBody] UpdateStockRequestDTO updateStock) {
        var stockModel = await _stockRepository.UpdateStockByIdAsync(id,updateStock);
        return stockModel != null ? Ok(stockModel) : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStockById([FromRoute] int id) {
        var stockModel = await _stockRepository.DeleteStockAsync(id);
        return  stockModel != null ? Ok(stockModel) : NotFound();;
    }
    
}