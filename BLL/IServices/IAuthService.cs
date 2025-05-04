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


namespace WindowsFormsApp1.BLL.IServices
{
    public interface IAuthService
    {
        bool IsValidEmail(string email);
        User Register(RegisterDTO registerRequest);
        User Login(LoginDTO loginRequest);
    }
}