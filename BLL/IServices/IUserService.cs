using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.DAL.Models;
using WindowsFormsApp1.DTOs;

namespace WindowsFormsApp1.BLL.IServices
{
    public interface IUserService
    {
        List<string> GetAllRoles();
        List<User> GetAllUsers();
        List<User> GetListUserByRoleAndUserName(string role, string username);
        List<User> GetListUserByRole(string role);
        List<User> GetListUserByUserName(string username);
        User GetUserById(Guid id);
        void UpdateUser(UserInformationDTO changeInformationDTO);
        void AddUser(UserInformationDTO changeInformationDTO);
        void DeleteUser(Guid userId);
    }
}
