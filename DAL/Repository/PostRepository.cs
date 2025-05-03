using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.DAL.Models;
namespace WindowsFormsApp1.DAL.Repository
{
    public class PostRepository
    {
        private readonly ApplicationDbContext _context;
        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddPost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }
        public Post FindPostById(Guid id)
        {
            return _context.Posts.Find(id);
        }
        public List<Post> GetAllPosts()
        {
            return _context.Posts.ToList();
        }
        public void UpdatePost(Post post)
        {
            _context.Entry(post).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }
        public void DeletePost(Post post)
        {
            _context.Posts.Remove(post);
            _context.SaveChanges();
        }
        public List<Post> GetPostsByCategory(Guid categoryId)
        {
            return _context.Posts.Where(p => p.CategoryId == categoryId).ToList();
        }
        public List<Post> GetPostsByUser(Guid userId)
        {
            return _context.Posts.Where(p => p.UserId == userId).ToList();
        }
        public List<Post> GetPostsByKeyword(string keyword)
        {
            return _context.Posts.Where(p => p.Title.Contains(keyword) || p.Content.Contains(keyword)).ToList();
        }
        public List<Post> GetPostsByStatus(PostStatus status)
        {
            return _context.Posts.Where(p => p.Status == status).ToList();
        }
        public List<Post> GetPostsByDateRange(DateTime startDate, DateTime endDate)
        {
            return _context.Posts.Where(p => p.CreatedAt >= startDate && p.CreatedAt <= endDate).ToList();
        }
        public List<Post> GetPostsByDate(DateTime date)
        {
            return _context.Posts.Where(p => p.CreatedAt.Date == date.Date).ToList();
        }
        public List<Post> GetPostsByDateAndStatus(DateTime date, PostStatus status)
        {
            return _context.Posts.Where(p => p.CreatedAt.Date == date.Date && p.Status == status).ToList();
        }
        List<string> GetAllPostStatus()
        {
            return Enum.GetNames(typeof(PostStatus)).ToList();
        }
        public string GetPostContentById(Guid postId)
        {
            var post = _context.Posts.Find(postId);
            return post != null ? post.Content : null;
        }
        public int UpdateResponseContent(Guid postId, string responseContent)
        {
            var post = _context.Posts.Find(postId);
            if (post != null)
            {
                post.ResponseContent = responseContent;
                post.RespondedAt = DateTime.Now;
                return _context.SaveChanges();
            }
            return 0;
        }
        public PostStatus GetPostStatusById(Guid postId)
        {
            var post = _context.Posts.Where(p => p.PostId == postId).Select(p => p.Status).FirstOrDefault();
            return post;
        }
        public Post GetPostWithStatus(Guid postId)
        {
            return _context.Posts.Include("Category")
                           .Include("User")
                           .FirstOrDefault(p => p.PostId == postId);
        }
        public List<Post> GetPostsByUserAndStatus(Guid userId, PostStatus status)
        {
            return _context.Posts.Where(p => p.UserId == userId && p.Status == status).ToList();
        }
        public List<Post> GetPostByStatus(PostStatus status)
        {
            return _context.Posts.Where(p => p.Status == status).ToList();
        }
        public int CountPost(Guid userId)
        {
            return _context.Posts.Count(p => p.UserId == userId);
        }
        public List<Post> GetPostsByFilter(Guid? userId, PostStatus? status)
        {
            var query = _context.Posts
                .Include("Category")
                .Include("User")
                .AsQueryable();
            if (userId.HasValue)
                query = query.Where(p => p.UserId == userId.Value);
            if (status.HasValue)
                query = query.Where(p => p.Status == status.Value);
            return query.ToList();
        }
    }
}
