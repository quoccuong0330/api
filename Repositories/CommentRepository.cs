using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTOs.Comment;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repositories;

public class CommentRepository :ICommentRepository {

    private readonly ApplicationDBContext _context;

    public CommentRepository(ApplicationDBContext context) {
        _context = context;
    }

    public async Task<List<Comment?>> GetAllCommentAsync() {
        return await _context.Comments.ToListAsync();
    }

    public async Task<Comment?> GetCommentByIdAsync(int idComment) {
        var isStockExist = await _context.Comments.FirstOrDefaultAsync(x=>x.Id==idComment);
        return isStockExist;
    }

    public async Task<Comment?> CreateNewCommentAsync(Comment newComment) {
         
         await _context.Comments.AddAsync(newComment);
         await _context.SaveChangesAsync();
         return newComment;
    }

    public async Task<Comment?> DeleteCommentAsync(int idComment) {
        var isCommentExist = await _context.Comments.FirstOrDefaultAsync(x => x.Id == idComment);
        if(isCommentExist==null) return null;
        _context.Comments.Remove(isCommentExist);
        await _context.SaveChangesAsync();
        return isCommentExist;
    }

    public async Task<Comment?> UpdateCommentAsync(int idComment, Comment updateComment) {
        var isCommentExist = await _context.Comments.FirstOrDefaultAsync(x => x.Id == idComment);
        if(isCommentExist==null) return null;
        isCommentExist.Content = updateComment.Content;
        isCommentExist.Title = updateComment.Title;
        isCommentExist.UpdateOn = DateTime.Now;
        await _context.SaveChangesAsync();
        return isCommentExist;
    }
}