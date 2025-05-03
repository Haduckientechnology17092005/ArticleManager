using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Session;

namespace WindowsFormsApp1.Presentation
{
    public partial class FormAuthor: Form
    {
        public FormAuthor()
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

        private void buttonProfile_Click(object sender, EventArgs e)
        {
            FormUserDetails formUserDetails = new FormUserDetails();
            formUserDetails.Show();
            this.Hide();
        }

        private void btnEditProfile_Click(object sender, EventArgs e)
        {
            if(UserSession.Instance.UserId.ToString() != null)
            {
                FormCreateEditUser formCreateEditUser = new FormCreateEditUser();
                formCreateEditUser.ShowDialog();
                System.Console.WriteLine($"User ID: {UserSession.Instance.UserId}");
                System.Console.WriteLine("Thanh cong !");
                formCreateEditUser.Text = "Edit Profile";
                //this.Hide();
            }
            else
            {
                MessageBox.Show("Please login to edit your profile.");
            }
        }

        private void btnPost_Click(object sender, EventArgs e)
        {
            FormPostManagement formPostManagement = new FormPostManagement();
            formPostManagement.Show();
            this.Hide();
        }
    }
}
