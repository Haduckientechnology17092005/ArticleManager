using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WindowsFormsApp1.DAL.Models
{
    public class User
    {
        // Khóa chính của bảng User
        [Key]
        public Guid UserId { get; set; } = Guid.NewGuid();
        // Các thuộc tính khác của người dùng
        [Required]  // Yêu cầu trường Username phải có giá trị
        public string Username { get; set; }

        [Required]  // Yêu cầu trường Password phải có giá trị
        public string Password { get; set; }

        public string Email { get; set; }

        // Quyền của người dùng dưới dạng chuỗi (ví dụ: "Admin", "Author", "Reader")
        public string Role { get; set; }
        //Tạo vào ngày tháng năm
        public DateTime CreatedAt { get; set; } = DateTime.Now; // Ngày tạo người dùng
        // Quan hệ với bảng Post (Author của bài viết)
        public bool IsDeleted { get; set; } = false; // Trạng thái xóa mềm
        public ICollection<Post> Posts { get; set; }

        // Quan hệ với bảng Comment (Reader hoặc Author có thể bình luận)
        public ICollection<Comment> Comments { get; set; }
    }
}
