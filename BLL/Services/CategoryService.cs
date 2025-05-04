using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.BLL.IServices;
using WindowsFormsApp1.DAL.Models;
using WindowsFormsApp1.DAL.Repository;

namespace WindowsFormsApp1.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly CategoryRepository _categoryRepository;
        public CategoryService(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public void AddCategory(Category category)
        {
            try
            {
                _categoryRepository.AddCategory(category);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Category FindCategoryById(Guid id)
        {
            try
            {
                return _categoryRepository.FindCategoryById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<string> GetAllCategoriesName()
        {
            try
            {
                return _categoryRepository.GetAllCategoriesName();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Category> GetAllCategories()
        {
            try
            {
                return _categoryRepository.GetAllCategories();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
