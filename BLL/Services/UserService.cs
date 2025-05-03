using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.DAL.Repository;
using WindowsFormsApp1.DAL.Models;
using WindowsFormsApp1.DTOs;

namespace WindowsFormsApp1.BLL.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public List<string> GetAllRoles()
        {
            try
            {
                var roles = _userRepository.GetAllRoles();
                return roles;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving roles: " + ex.Message);
            }
        }
        public List<User> GetAllUsers()
        {
            try
            {
                var users = _userRepository.GetAllUsers();
                return users.Where(u => !u.IsDeleted).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving users: " + ex.Message);
            }
        }
        public List<User> GetListUserByRoleAndUserName(string role, string username)
        {
            try
            {
                var users = _userRepository.GetAllUsers().Where(u => !u.IsDeleted);
                if (!string.IsNullOrEmpty(role))
                {
                    users = users.Where(u => u.Role == role).ToList();
                }
                if (!string.IsNullOrEmpty(username))
                {
                    users = users.Where(u => u.Username.Contains(username)).ToList();
                }
                return users.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving users: " + ex.Message);
            }
        }
        public List<User> GetListUserByRole(string role)
        {
            try
            {
                var users = _userRepository.GetAllUsers().Where(u => !u.IsDeleted);
                if (!string.IsNullOrEmpty(role))
                {
                    users = users.Where(u => u.Role == role).ToList();
                }
                return users.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving users: " + ex.Message);
            }
        }
        public List<User> GetListUserByUserName(string username)
        {
            try
            {
                var users = _userRepository.GetAllUsers().Where(u => !u.IsDeleted);
                if (!string.IsNullOrEmpty(username))
                {
                    users = users.Where(u => u.Username.Contains(username)).ToList();
                }
                return users.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving users: " + ex.Message);
            }
        }
        public User GetUserById(Guid id)
        {
            try
            {
                var user = _userRepository.FindUserById(id);
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving user: " + ex.Message);
            }
        }
        public void UpdateUser(UserInformationDTO changeInformationDTO)
        {
            try
            {
                var user = _userRepository.FindUserById(changeInformationDTO.UserId);
                if (user == null)
                {
                    throw new Exception("User not found.");
                }
                if (!string.IsNullOrEmpty(changeInformationDTO.NewPassword))
                {
                    user.Password = BCrypt.Net.BCrypt.HashPassword(changeInformationDTO.NewPassword);
                }
                if (!string.IsNullOrEmpty(changeInformationDTO.Email))
                {
                    var existingEmail = _userRepository.FindUserByEmail(changeInformationDTO.Email);
                    if (existingEmail != null && existingEmail.UserId != user.UserId)
                    {
                        throw new Exception("Email already exists.");
                    }
                    user.Email = changeInformationDTO.Email;
                }
                if (!string.IsNullOrEmpty(changeInformationDTO.Username))
                {
                    var existingUser = _userRepository.FindUserByUsername(changeInformationDTO.Username);
                    if (existingUser != null && existingUser.UserId != user.UserId)
                    {
                        throw new Exception("Username already exists.");
                    }
                    user.Username = changeInformationDTO.Username;
                }
                if(!string.IsNullOrEmpty(changeInformationDTO.Role))
                {
                    user.Role = changeInformationDTO.Role;
                }
                else
                {
                    throw new Exception("Role must be required");
                }    
                //System.Console.WriteLine("Thong tin cu the Update");
                //System.Console.WriteLine(user.Username);
                //System.Console.WriteLine(changeInformationDTO.NewPassword);
                //System.Console.WriteLine(user.Password);
                //System.Console.WriteLine(user.Email)
                //System.Console.WriteLine(user.Role);
                //System.Console.WriteLine(user.CreatedAt);
                //System.Console.WriteLine(user.UserId);
                _userRepository.UpdateUser(user);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating user: " + ex.Message);
            }
        }
        public void AddUser(UserInformationDTO changeInformationDTO)
        {
            try
            {
                var existingUser = _userRepository.FindUserByUsername(changeInformationDTO.Username);
                if (existingUser != null)
                {
                    throw new Exception("Username already exists.");
                }
                var existingEmail = _userRepository.FindUserByEmail(changeInformationDTO.Email);
                if (existingEmail != null)
                {
                    throw new Exception("Email already exists.");
                }
                if(string.IsNullOrEmpty(changeInformationDTO.Role))
                {
                    throw new Exception("Role is required.");
                }
                var user = new User
                {
                    Username = changeInformationDTO.Username,
                    Password = BCrypt.Net.BCrypt.HashPassword(changeInformationDTO.Password),
                    Email = changeInformationDTO.Email,
                    Role = changeInformationDTO.Role
                };
                _userRepository.AddUser(user);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding user: " + ex.Message);
            }
        }
        public void DeleteUser(Guid userId)
        {
            try
            {
                _userRepository.SoftDeleteUser(userId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting user: " + ex.Message);
            }
        }
    }
}
