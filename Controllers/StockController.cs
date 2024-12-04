using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTOs.Stock;
using WebAPI.Helpers;
using WebAPI.Interfaces;
using WebAPI.Mappers;


namespace WebAPI.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController : ControllerBase {
    private readonly IStockRepository _stockRepository;

    public StockController(ApplicationDBContext dbContext,IStockRepository stockRepository) {
        _stockRepository = stockRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] QueryObject query) {
        var stocks = await _stockRepository.GetAllAsync(query);
        var stocksDto = stocks.Select(s => s.ToStockDto());
        return Ok(stocksDto);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetStockById([FromRoute] int id) {
        var stockModel = await _stockRepository.GetByIdAsync(id);
        return stockModel != null ? Ok(stockModel) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewStock([FromBody] CreateStockRequestDTO createStock) {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var stockModel = await _stockRepository.CreateNewStockAsync(createStock.ToStockFromCreateDTO());
        return CreatedAtAction(nameof(GetStockById), new {id = stockModel.Id}, stockModel.ToStockDto());
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateStockById([FromRoute] int id, [FromBody] UpdateStockRequestDTO updateStock) {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var stockModel = await _stockRepository.UpdateStockByIdAsync(id,updateStock);
        return stockModel != null ? Ok(stockModel) : NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteStockById([FromRoute] int id) {
        var stockModel = await _stockRepository.DeleteStockAsync(id);
        return  stockModel != null ? NoContent() : NotFound();;
    }
    
}