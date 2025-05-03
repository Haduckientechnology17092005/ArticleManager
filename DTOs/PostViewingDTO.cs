using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.DTOs
{
    public class PostViewingDTO
    {
        public Guid PostId { get; set; } = Guid.NewGuid();
        public Guid AuthorId {  get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string AuthorName { get; set; }
        public string Status { get; set; }
        public string Content { get; set; }
        public string Response { get; set; } = String.Empty;
        public List<CommentDTO> Comments { get; set; }
    }
}
