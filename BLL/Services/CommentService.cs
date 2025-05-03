using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.DAL.Models;
using WindowsFormsApp1.DAL.Repository;
using WindowsFormsApp1.DTOs;
using WindowsFormsApp1.Session;

namespace WindowsFormsApp1.BLL.Services
{
    public class CommentService
    {
        private readonly CommentRepository _commentRepository;
        public 
            CommentService(CommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public void AddComment(Comment comment)
        {
            _commentRepository.AddComment(comment);
        }
        public Comment FindCommentById(Guid id)
        {
            return _commentRepository.FindCommentById(id);
        }
        public List<Comment> GetAllComments()
        {
            return _commentRepository.GetAllComments();
        }
        public void UpdateComment(Comment comment)
        {
            _commentRepository.UpdateComment(comment);
        }
        public void DeleteComment(Guid commentId)
        {
            try
            {
                // First get the comment from database
                var comment = _commentRepository.FindCommentById(commentId);
                Console.WriteLine(commentId);
                _commentRepository.DeleteComment(comment);
                _commentRepository.Save();
            }
            catch (Exception ex)
            {
                // Log the exception
                // You might want to use a logging framework here
                Console.WriteLine($"Error deleting comment: {ex.Message}");
            }
        }
        public List<Comment> GetCommentsByPost(Guid postId)
        {
            return _commentRepository.GetCommentsByPost(postId);
        }
        public List<Comment> GetCommentsByUser(Guid userId)
        {
            return _commentRepository.GetCommentsByUser(userId);
        }
    }
}
