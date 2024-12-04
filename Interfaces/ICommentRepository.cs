using WebAPI.DTOs.Comment;
using WebAPI.Models;

namespace WebAPI.Interfaces;

public interface ICommentRepository {
    Task<List<Comment?>> GetAllCommentAsync();
    Task<Comment?> GetCommentByIdAsync(int idComment);
    Task<Comment?> CreateNewCommentAsync(Comment newComment);
    Task<Comment?> DeleteCommentAsync(int idComment);
    Task<Comment?> UpdateCommentAsync(int idComment, Comment updateComment);
}