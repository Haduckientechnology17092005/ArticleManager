using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.BLL.IServices;
using WindowsFormsApp1.DAL.Models;
using WindowsFormsApp1.DAL.Repository;
using WindowsFormsApp1.DTOs;
using WindowsFormsApp1.Session;

namespace WindowsFormsApp1.BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly CommentRepository _commentRepository;
        public CommentService(CommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public void AddComment(Comment comment)
        {
            try
            {
                _commentRepository.AddComment(comment);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Comment FindCommentById(Guid id)
        {
            try
            {
                return _commentRepository.FindCommentById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Comment> GetAllComments()
        {
            try
            {
                return _commentRepository.GetAllComments();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateComment(Comment comment)
        {
            try
            {
                _commentRepository.UpdateComment(comment);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
            try
            {
                return _commentRepository.GetCommentsByPost(postId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Comment> GetCommentsByUser(Guid userId)
        {
            try
            {
                return _commentRepository.GetCommentsByUser(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
