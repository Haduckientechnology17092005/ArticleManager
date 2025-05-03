using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.BLL.Services;
using WindowsFormsApp1.DTOs;
using WindowsFormsApp1.DAL.Models;
using WindowsFormsApp1.DAL.Repository;
using WindowsFormsApp1.Session;

namespace WindowsFormsApp1.Presentation
{
    public partial class FormLogin: Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            var loginRequest = new LoginDTO
            {
                UserName = txtUserName.Text,
                Password = txtPassword.Text
            };
            try
            {
                var userService = new AuthService(new UserRepository(new ApplicationDbContext()));
                var loginResult = userService.Login(loginRequest);
                if (loginResult != null)
                {
                    UserSession.Instance.SetUserSession(loginResult.UserId, loginResult.Username, loginResult.Role);
                    MessageBox.Show("Login successful");
                }
                else
                {
                    MessageBox.Show("Login failed");
                }
                System.Console.WriteLine($"User ID: {UserSession.Instance.UserId}");
                System.Console.WriteLine($"Username: {UserSession.Instance.Username}");
                System.Console.WriteLine($"Role: {UserSession.Instance.Role}");
                if(UserSession.Instance.Role == "Admin")
                {
                    FormAdmin formAdmin = new FormAdmin();
                    formAdmin.Show();
                    this.Hide();
                }
                else if (UserSession.Instance.Role == "Author")
                {
                    FormAuthor formAuthor = new FormAuthor();
                    formAuthor.Show();
                    this.Hide();
                }
                else if (UserSession.Instance.Role == "Reader")
                {
                    FormReader formReader = new FormReader();
                    formReader.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Quên mật khẩu? Vui lòng liên hệ với quản trị viên để được hỗ trợ: 0387717425.");
            Application.Exit();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            FormRegister formRegister = new FormRegister();
            formRegister.Show();
            this.Hide();
        }
    }
}
