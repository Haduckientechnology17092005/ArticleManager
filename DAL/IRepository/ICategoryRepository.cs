using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.DAL.Models;

namespace WindowsFormsApp1.DAL.IRepository
{
    public interface ICategoryRepository
    {
        void AddCategory(Category category);
        Category FindCategoryById(Guid id);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
        List<Category> GetCategoriesByPost(Guid postId);
        List<Category> GetCategoriesByUser(Guid userId);
        List<Category> GetCategoriesByKeyword(string keyword);
        List<Category> GetCategoriesByPopularity(int minPosts);
        List<string> GetAllCategoriesName();
        List<Category> GetAllCategories();
    }
}
