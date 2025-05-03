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

namespace WindowsFormsApp1.Presentation
{
    public partial class FormRegister: Form
    {
        public FormRegister()
        {
            InitializeComponent();
        }

        private string GetSelectedRole()
        {
            if (rbtnAuthor.Checked)
                return "Author";
            else if (rbtnReader.Checked)
                return "Reader";
            else
                return String.Empty;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text;
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;
            string email = txtEmail.Text;
            string role = GetSelectedRole();
            if(!password.Equals(confirmPassword))
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }
            try
            {
                // Assuming you have a method to register the user
                var userService = new AuthService(new UserRepository(new ApplicationDbContext()));
                var requestDTO = new RegisterDTO
                {
                    UserName = username,
                    Password = password,
                    ConfirmPassword = confirmPassword,
                    Role = role,
                    Email = email
                };

                var registerResult = userService.Register(requestDTO);
                if(registerResult != null)
                {
                    MessageBox.Show("Registration successful");
                    FormLogin formLogin = new FormLogin();
                    formLogin.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Registration failed");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            formLogin.Show();
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
