using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.DAL.Models;
using WindowsFormsApp1.DAL.Repository;

namespace WindowsFormsApp1.BLL.Services
{
    public class CategoryService
    {
        private readonly CategoryRepository _categoryRepository;
        public CategoryService(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public void AddCategory(Category category)
        {
            _categoryRepository.AddCategory(category);
        }
        public Category FindCategoryById(Guid id)
        {
            return _categoryRepository.FindCategoryById(id);
        }
        public List<string> GetAllCategoriesName()
        {
            return _categoryRepository.GetAllCategoriesName();
        }
        public List<Category> GetAllCategories()
        {
            return _categoryRepository.GetAllCategories();
        }
    }
}
