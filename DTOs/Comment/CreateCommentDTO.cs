using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs.Comment;

public class CreateCommentDTO {
    [Required]
    [MinLength(3, ErrorMessage = "Title must be 3 characters")]
    [MaxLength(100, ErrorMessage = "Title cannot be over 100 characters")]
    public string Title { get; set; } = string.Empty;
    [Required]
    [MinLength(50, ErrorMessage = "Content must be 50 characters")]
    [MaxLength(1000, ErrorMessage = "Content cannot be over 1000 characters")]
    public string Content { get; set; } = string.Empty;
}