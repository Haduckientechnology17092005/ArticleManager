using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.DAL.Models;
namespace WindowsFormsApp1.DAL.Repository
{
    public class CommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }
        public Comment FindCommentById(Guid id)
        {
            return _context.Comments.Find(id);
        }
        public List<Comment> GetAllComments()
        {
            return _context.Comments.ToList();
        }
        public void UpdateComment(Comment comment)
        {
            _context.Entry(comment).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }
        public void DeleteComment(Comment comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment), "Comment không tồn tại.");
            }

            _context.Comments.Remove(comment);
            _context.SaveChanges();
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public List<Comment> GetCommentsByPost(Guid postId)
        {
            return _context.Comments.Where(c => c.PostId == postId).ToList();
        }
        public List<Comment> GetCommentsByUser(Guid userId)
        {
            return _context.Comments.Where(c => c.UserId == userId).ToList();
        }
    }
}
