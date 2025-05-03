using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WindowsFormsApp1.DAL.Models
{
    public enum PostStatus
    {
        Pending,    // Chờ duyệt
        Approved,   // Đã duyệt
        Rejected    // Bị từ chối
    }
    public class Post
    {
        // Khóa chính của bảng Post
        [Key]
        public Guid PostId { get; set; } = Guid.NewGuid(); 
        // Tiêu đề bài viết
        [Required]
        public string Title { get; set; }
        // Nội dung bài viết
        [Required]
        public string Content { get; set; }
        // Khóa ngoại cho danh mục (Category)
        public Guid CategoryId { get; set; }
        //Tao vao ngay thang nam
        public DateTime CreatedAt { get; set; } = DateTime.Now; // Ngày tạo bài viết
        // Mối quan hệ với Category
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        //Trạng thái bài viết
        public PostStatus Status { get; set; } = PostStatus.Pending; // Trạng thái bài viết (Pending, Approved, Rejected)
        //Ngày phản hồi
        public DateTime? RespondedAt { get; set; } // Ngày phản hồi (nếu có)
        //Nội dung phản hồi
        public string ResponseContent { get; set; } = String.Empty; // Nội dung phản hồi (nếu có)
        // Trạng thái xóa mềm
        public bool IsDeleted { get; set; } = false; // Trạng thái xóa mềm
        // Ngày xóa bài viết
        public DateTime? DeletedAt { get; set; } // Ngày xóa bài viết (nếu có)
        // Khóa ngoại cho User (Author)
        public Guid UserId { get; set; }
        // Mối quan hệ với User (Author của bài viết)
        [ForeignKey("UserId")]
        public User User { get; set; }
        // Quan hệ với Comment
        public ICollection<Comment> Comments { get; set; }
        public Post()
        {
            // Khởi tạo Comments để tránh lỗi null khi làm việc với mối quan hệ
            Comments = new HashSet<Comment>();
        }
    }
}
