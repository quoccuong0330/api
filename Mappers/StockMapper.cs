using WebAPI.DTOs.Stock;
using WebAPI.Models;

namespace WebAPI.Mappers;

public static class StockMapper {
    public static StockDTO ToStockDto(this Stock stockModel) {
        return new StockDTO {
            Id = stockModel.Id,
            Symbol = stockModel.Symbol,
            LastDiv = stockModel.LastDiv,
            CompanyName = stockModel.CompanyName,
            Purchase = stockModel.Purchase,
            Industry = stockModel.Industry,
            MarketCap = stockModel.MarketCap,
        };

    }
}