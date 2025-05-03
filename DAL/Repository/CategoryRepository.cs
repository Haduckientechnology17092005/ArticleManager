using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.DAL.Models;
namespace WindowsFormsApp1.DAL.Repository
{
    public class CategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }
        public Category FindCategoryById(Guid id)
        {
            return _context.Categories.Find(id);
        }
        public void UpdateCategory(Category category)
        {
            _context.Entry(category).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }
        public void DeleteCategory(Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
        public List<Category> GetCategoriesByPost(Guid postId)
        {
            return _context.Categories.Where(c => c.Posts.Any(p => p.PostId == postId)).ToList();
        }
        public List<Category> GetCategoriesByUser(Guid userId)
        {
            return _context.Categories.Where(c => c.Posts.Any(p => p.UserId == userId)).ToList();
        }
        public List<Category> GetCategoriesByKeyword(string keyword)
        {
            return _context.Categories.Where(c => c.Name.Contains(keyword)).ToList();
        }
        public List<Category> GetCategoriesByPopularity(int minPosts)
        {
            return _context.Categories.Where(c => c.Posts.Count >= minPosts).ToList();
        }
        public List<string> GetAllCategoriesName()
        {
            var categories = _context.Categories
                .Select(c => c.Name)
                .ToList();
            return categories;
        }
        public List<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }
    }
}
