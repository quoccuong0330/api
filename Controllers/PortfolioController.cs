using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Extensions;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Controllers;

[Route("api/portfolio")]
[ApiController]
public class PortfolioController : ControllerBase {
    private readonly UserManager<User> _userManager;
    private readonly IStockRepository _stockRepository;
    private readonly IPortfolioRepository _portfolioRepository;
    
    public PortfolioController(UserManager<User> userManager, IStockRepository stockRepository, IPortfolioRepository portfolioRepository) {
        _userManager = userManager;
        _stockRepository = stockRepository;
        _portfolioRepository = portfolioRepository;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserPortfolio() {
        var username = User.FindFirstValue(ClaimTypes.GivenName);
        var appUser = await _userManager.FindByNameAsync(username);
        if (string.IsNullOrEmpty(appUser.Id)) return Unauthorized("User not authenticated or userId claim not found.");
        var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
        return Ok(userPortfolio);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateUserPortfolio(string symbol) {
        var username = User.FindFirstValue(ClaimTypes.GivenName);
        var appUser = await _userManager.FindByNameAsync(username);
        if (string.IsNullOrEmpty(appUser.Id)) return Unauthorized("User not authenticated or userId claim not found.");
        var stock = await _stockRepository.GetBySymbolAsync(symbol);
        if (stock == null) return BadRequest("Stock not found");
        var hasExists = await _portfolioRepository.GetUserPortfolio(appUser);
        if (hasExists.Any(x => x.Symbol.ToLower() == symbol.ToLower())) 
            return BadRequest("This symbol has exists");
        var portfolioModel = new Portfolio {
            stockId = stock.Id,
            userId = appUser.Id
        };
        await _portfolioRepository.AddStockToPortfolio(portfolioModel);
        return portfolioModel == null ? StatusCode(500, "Could not create") : Created();
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteStockFromPortfolio(string symbol) {
        var username = User.FindFirstValue(ClaimTypes.GivenName);
        var appUser = await _userManager.FindByNameAsync(username);
        if (string.IsNullOrEmpty(appUser.Id)) return Unauthorized("User not authenticated or userId claim not found.");
        var stock = await _stockRepository.GetBySymbolAsync(symbol);
        if (stock == null) return BadRequest("Stock not found");
        var hasExists = await _portfolioRepository.GetUserPortfolio(appUser);
        if (hasExists.All(x => x.Symbol.ToLower() != symbol.ToLower())) 
            return BadRequest("This symbol does not exists");
        await _portfolioRepository.DeleteStockFromPortfolio(appUser, symbol);
        return NoContent();
    }
    
}