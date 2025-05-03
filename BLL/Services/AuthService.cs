using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.DAL.Models;
using WindowsFormsApp1.DTOs;
using WindowsFormsApp1.DAL.Repository;
using BCrypt.Net;
using System.Text.RegularExpressions;

namespace WindowsFormsApp1.BLL.Services
{
    public class AuthService
    {
        private readonly UserRepository _userRepository;
        public AuthService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public bool IsValidEmail(string email)
        {
            // Biểu thức chính quy kiểm tra email
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }

        public User Register(RegisterDTO registerRequest)
        {
            try
            {
                var existingUser = _userRepository.FindUserByUsername(registerRequest.UserName);
                if (existingUser != null)
                {
                    throw new Exception("Username already exists");
                }
                if (!IsValidEmail(registerRequest.Email))
                {
                    throw new Exception("Invalid email format");
                }
                var existingEmail = _userRepository.FindUserByEmail(registerRequest.Email);
                if (existingEmail != null)
                {
                    throw new Exception("Email already exists");
                }
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password);
                var newUser = new User
                {
                    Username = registerRequest.UserName,
                    Password = hashedPassword,
                    Role = registerRequest.Role,
                    Email = registerRequest.Email
                };

                _userRepository.AddUser(newUser);
                return newUser;
            }
            catch (Exception ex)
            {
                throw new Exception($"Registration failed: {ex.Message}");
            }
        }
        public User Login(LoginDTO loginRequest)
        {
            try
            {
                var user = _userRepository.FindUserByUsername(loginRequest.UserName);
                if (user == null || user.IsDeleted == true)
                {
                    throw new Exception("User not found");
                }
                if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password))
                {
                    throw new Exception("Invalid password");
                }
                
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"Login failed: {ex.Message}");
            }
        }
    }
}
