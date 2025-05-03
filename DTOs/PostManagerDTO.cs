using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WindowsFormsApp1.DTOs
{
    public class PostManagerDTO
    {
        public Guid PostId { get; set; } = Guid.NewGuid();
        public string PostName { get; set; }
        public string CategoryName { get; set; }
        public string AuthorName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int CountComment { get; set; } = 0;
        public string Status { get; set; } = "Pending";
    }
}
