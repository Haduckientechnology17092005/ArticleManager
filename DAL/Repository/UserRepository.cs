using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.DAL.Models;
namespace WindowsFormsApp1.DAL.Repository
{
    public class UserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public User FindUserById(Guid id)
        {
            return _context.Users.Find(id);
        }
        public User FindUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

        public User FindUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }
        public void UpdateUser(User user)
        {
            _context.Entry(user).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }
        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
        public List<string> GetAllRoles()
        {
            return _context.Users.Select(u => u.Role).Distinct().ToList();
        }
        public void SoftDeleteUser(Guid userId)
        {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                user.IsDeleted = true; // Đánh dấu người dùng là đã xóa mềm
                _context.SaveChanges();
            }
        }
    }
}
