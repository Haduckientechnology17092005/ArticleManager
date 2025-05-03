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
using WindowsFormsApp1.Session;

namespace WindowsFormsApp1.Presentation
{
    public partial class FormAdmin: Form
    {
        public FormAdmin()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UserSession.Instance.ClearUserSession();
            System.Console.WriteLine($"User ID: {UserSession.Instance.UserId}");
            System.Console.WriteLine($"Username: {UserSession.Instance.Username}");
            System.Console.WriteLine($"Role: {UserSession.Instance.Role}");
            FormLogin formLogin = new FormLogin();
            formLogin.Show();
            this.Close();
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            if(UserSession.Instance.Role == "Admin")
            {
                FormUserManagement formUserManagement = new FormUserManagement();
                formUserManagement.Show();
                this.Hide();
            } else
            {
                MessageBox.Show("You do not have permission to access this feature.");
            }
        }

        private void btnPostManager_Click(object sender, EventArgs e)
        {
            FormPostManagement formPostManagement = new FormPostManagement();
            formPostManagement.Show();
            this.Hide();
        }
    }
}
