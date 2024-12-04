using WebAPI.DTOs.Comment;
using WebAPI.Models;

namespace WebAPI.Mappers;

public static class CommentMapper {
    public static CommentDTO toCommentDTO(this Comment commentModel) {
        return new CommentDTO {
            Id = commentModel.Id,
            Title = commentModel.Title,
            Content = commentModel.Content,
            CreateOn = commentModel.CreateOn,
            UpdateOn = commentModel.UpdateOn,
            StockId = commentModel.StockId,
        };
    }

    public static Comment toCommentFromCreateDTO(this CreateCommentDTO commentModel, int stockId) {
        return new Comment {
            Title = commentModel.Title,
            Content = commentModel.Content,
            StockId = stockId
           
        };
    }
    
    public static Comment toCommentFromUpdateDTO(this UpdateCommentDTO commentModel) {
        return new Comment {
            Title = commentModel.Title,
            Content = commentModel.Content,
            UpdateOn = DateTime.Now
           
        };
    }
}