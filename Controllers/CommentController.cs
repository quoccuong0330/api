using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.DTOs.Comment;
using WebAPI.Interfaces;
using WebAPI.Mappers;
using WebAPI.Models;

namespace WebAPI.Controllers;
[Route("api/comment")]
[ApiController]
public class CommentController : ControllerBase {
    private readonly ICommentRepository _commentRepository;
    private readonly IStockRepository _stockRepository;

    public CommentController(IStockRepository stockRepository, ICommentRepository commentRepository) {
        _commentRepository = commentRepository;
        _stockRepository = stockRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllComment() {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var listComment = await _commentRepository.GetAllCommentAsync();
        var commentDto = listComment.Select (c => c.toCommentDTO());
        return Ok(commentDto);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCommentById([FromRoute] int id) {
        var listComment = await _commentRepository.GetCommentByIdAsync(id);
        return listComment == null ? NotFound() : Ok(listComment);
    }

    [HttpPost("{stockId:int}")]
    public async Task<IActionResult> CreteNewComment([FromRoute] int stockId,[FromBody] CreateCommentDTO commentDto) {
        if (!await _stockRepository.isStockExists(stockId)) {
            return BadRequest("Stock does not exist.");
        }

        if (!ModelState.IsValid) return BadRequest(ModelState);
        var commentModel = commentDto.toCommentFromCreateDTO(stockId);
        await _commentRepository.CreateNewCommentAsync(commentModel);
        return CreatedAtAction(nameof(GetCommentById), new {id = commentModel.Id}, commentModel.toCommentDTO());
    }

    [HttpPut("{commentId:int}")]
    public async Task<IActionResult> UpdateComment([FromRoute] int commentId, [FromBody] UpdateCommentDTO commentDto) {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var commentModel = commentDto.toCommentFromUpdateDTO();
        var result = await _commentRepository.UpdateCommentAsync(commentId, commentModel);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{commentId:int}")]
    public async Task<IActionResult> DeleteComment([FromRoute] int commentId) {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _commentRepository.DeleteCommentAsync(commentId);
        return result == null ? NotFound() : NoContent();

    }
}