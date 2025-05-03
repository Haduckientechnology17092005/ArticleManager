using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WindowsFormsApp1.DAL.Models
{
    public class Category
    {
        // Khóa chính của bảng Category
        [Key]
        public Guid CategoryId { get; set; } = Guid.NewGuid();

        // Tên danh mục
        [Required]
        [MaxLength(100)] // Giới hạn độ dài tối đa của tên danh mục
        public string Name { get; set; }

        // Quan hệ một-nhiều với Post
        public ICollection<Post> Posts { get; set; }

        public Category()
        {
            // Khởi tạo Posts tránh lỗi null khi làm việc với quan hệ
            Posts = new HashSet<Post>();
        }
    }
}
