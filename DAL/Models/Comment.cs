using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WindowsFormsApp1.DAL.Models
{
    public class Comment
    {
        // Khóa chính của bảng Comment
        [Key]
        public Guid CommentId { get; set; } = Guid.NewGuid();

        // Nội dung bình luận
        [Required]
        public string Content { get; set; }
        // Tạo vào ngày tháng năm
        public DateTime CreatedAt { get; set; } = DateTime.Now; // Ngày tạo bình luận
        // Khóa ngoại cho Post
        public Guid PostId { get; set; }

        // Mối quan hệ với bảng Post
        [ForeignKey("PostId")]
        public Post Post { get; set; }

        // Khóa ngoại cho User (Reader hoặc Author hoặc Admin)
        public Guid UserId { get; set; }

        // Mối quan hệ với bảng User
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
