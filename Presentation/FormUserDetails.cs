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
using WindowsFormsApp1.BLL.Services;
using WindowsFormsApp1.DAL.Repository;
using WindowsFormsApp1.DAL.Models;
using System.Data.Entity.Infrastructure;

namespace WindowsFormsApp1.Presentation
{
    public partial class FormUserDetails : Form
    {
        public FormUserDetails()
        {
            InitializeComponent();
            LoadDataUser();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (UserSession.Instance.Role == "Admin")
            {
                FormUserManagement formUserManagement = new FormUserManagement();
                formUserManagement.Show();
                this.Close();
            }
            else if (UserSession.Instance.Role == "Author")
            {
                FormAuthor formAuthor = new FormAuthor();
                formAuthor.Show();
                this.Close();
            }
            else if (UserSession.Instance.Role == "Reader")
            {
                FormReader formReader = new FormReader();
                formReader.Show();
                this.Close();
            }
        }
        private void LoadDataUser()
        {
            try
            {
                var userService = new UserService(new UserRepository(new ApplicationDbContext()));
                var user = userService.GetUserById(UserSession.Instance.UserId);
                txtUserId.Text = UserSession.Instance.UserId.ToString();
                txtUserName.Text = user.Username.ToString();
                txtEmail.Text = user.Email.ToString();
                txtRole.Text = user.Role.ToString();
                txtCreatedAt.Text = user.CreatedAt.ToString();
                txtPassword.Text = user.Password.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu người dùng: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Show confirmation dialog
                DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa tài khoản này? Thao tác này không thể hoàn tác!",
                    "Xác nhận xóa",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2); 

                if (result == DialogResult.OK)
                {
                    UserService userService = new UserService(new UserRepository(new ApplicationDbContext()));
                    var user = userService.GetUserById(UserSession.Instance.UserId);

                    // Additional check to prevent accidental self-deletion
                    if (user != null && user.UserId == UserSession.Instance.UserId)
                    {
                        userService.DeleteUser(UserSession.Instance.UserId);
                        UserSession.Instance.ClearUserSession();
                        MessageBox.Show("Xóa người dùng thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        FormLogin formLogin = new FormLogin();
                        formLogin.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa người dùng này!", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Đã hủy thao tác xóa.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (DbUpdateException dbEx)
            {
                MessageBox.Show($"Lỗi cơ sở dữ liệu: {dbEx.InnerException?.Message ?? dbEx.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa người dùng: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
