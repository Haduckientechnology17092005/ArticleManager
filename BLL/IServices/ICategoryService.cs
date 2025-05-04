using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.DAL.Models;

namespace WindowsFormsApp1.BLL.IServices
{
    public interface ICategoryService
    {
        void AddCategory(Category category);
        Category FindCategoryById(Guid id);
        List<string> GetAllCategoriesName();
        List<Category> GetAllCategories();
    }
}
