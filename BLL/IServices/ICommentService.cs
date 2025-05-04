using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.DAL.Models;

namespace WindowsFormsApp1.BLL.IServices
{
    public interface ICommentService
    {
        void AddComment(Comment comment);
        Comment FindCommentById(Guid id);
        List<Comment> GetAllComments();
        void UpdateComment(Comment comment);
        void DeleteComment(Guid commentId);
        List<Comment> GetCommentsByPost(Guid postId);
        List<Comment> GetCommentsByUser(Guid userId);
    }
}
