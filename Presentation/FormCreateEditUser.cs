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
using WindowsFormsApp1.DAL.Models;
using WindowsFormsApp1.DAL.Repository;
using WindowsFormsApp1.DTOs;
using WindowsFormsApp1.Session;
using BCrypt.Net;
using System.Data.Entity.Infrastructure;

namespace WindowsFormsApp1.Presentation
{
    public partial class FormCreateEditUser: Form
    {
        public new delegate void Load(DataTable li);
        private Load _loadDGV;
        private Guid _userId { get; set; }
        public FormCreateEditUser(Guid UserId, Load LoadDGV)
        {
            InitializeComponent();
            _userId = UserId;
            _loadDGV = LoadDGV;
            GUI();
        }
        public void GUI()
        {
            try
            {
                if (_userId == Guid.Empty)
                {
                    txtUserId.Text = string.Empty;
                    txtUsername.Text = string.Empty;
                    txtEmail.Text = string.Empty;
                    rbtnAdmin.Checked = false;
                    rbtnAuthor.Checked = false;
                    rbtnReader.Checked = false;
                    txtCreatedAt.Text = string.Empty;
                    txtPassword.Text = string.Empty;
                    txtNewPassword.Text = string.Empty;
                    txtNewPassword.ReadOnly = true;
                }
                else
                {
                    LoadGUI(_userId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        public DataTable GetRecords()
        {
            try
            {
                var userService = new UserService(new UserRepository(new ApplicationDbContext()));
                var postService = new PostService(new PostRepository(new ApplicationDbContext()));
                var users = userService.GetAllUsers();
                var posts = postService.GetAllPosts();

                // Create a DataTable to store the data
                var dataTable = new DataTable();
                dataTable.Columns.Add("UserId", typeof(Guid));
                dataTable.Columns.Add("Username", typeof(string));
                dataTable.Columns.Add("Email", typeof(string));
                dataTable.Columns.Add("Role", typeof(string));
                dataTable.Columns.Add("Password", typeof(string));
                dataTable.Columns.Add("CreatedAt", typeof(DateTime));
                dataTable.Columns.Add("PostCount", typeof(int));

                // Populate the DataTable with data
                foreach (var user in users)
                {
                    var postCount = posts.Count(p => p.UserId == user.UserId);
                    dataTable.Rows.Add(
                        user.UserId,
                        user.Username,
                        user.Email,
                        user.Role,
                        user.Password,
                        user.CreatedAt,
                        postCount
                    );
                }
                //fix was null
                if (dataTable.Rows.Count == 0)
                {
                    dataTable.Rows.Add(Guid.Empty, string.Empty, string.Empty, 0, string.Empty, string.Empty, DateTime.MinValue);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public FormCreateEditUser()
        {
            InitializeComponent();
            LoadGUI(UserSession.Instance.UserId);
        }
        public void LoadGUI(Guid userId)
        {
            try
            {
                var userService = new UserService(new UserRepository(new ApplicationDbContext()));
                var user = userService.GetUserById(userId);
                txtUserId.Text = user.UserId.ToString();
                txtUsername.Text = user.Username.ToString();
                txtEmail.Text = user.Email.ToString();
                rbtnAdmin.Enabled = false;
                rbtnAuthor.Enabled = false;
                rbtnReader.Enabled = false;
                if (user.Role == "Admin")
                {
                    rbtnAdmin.Checked = true;
                    rbtnAdmin.Enabled = true;
                }
                else if (user.Role == "Author")
                {
                    rbtnAuthor.Checked = true;
                    rbtnAuthor.Enabled = true;
                }
                else if (user.Role == "Reader")
                {
                    rbtnReader.Checked = true;
                    rbtnReader.Enabled = true;
                }
                //Nếu là admin chỉnh sửa trong phiên đăng nhập thì cho phép chỉnh sửa role
                if (UserSession.Instance.Role == "Admin")
                {
                    rbtnAdmin.Enabled = true;
                    rbtnAuthor.Enabled = true;
                    rbtnReader.Enabled = true;
                }
                txtCreatedAt.Text = user.CreatedAt.ToString();
                txtPassword.Text = user.Password.ToString();
                txtPassword.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var userService = new UserService(new UserRepository(new ApplicationDbContext()));
                var user = new UserInformationDTO
                {
                    Username = txtUsername.Text,
                    Email = txtEmail.Text,
                    Role = rbtnAdmin.Checked ? "Admin":rbtnAuthor.Checked ? "Author" :rbtnReader.Checked ? "Reader":string.Empty,
                };

                // Xác định hành động (Thêm/Cập nhật)
                if (_userId != Guid.Empty)
                {
                    user.UserId = _userId;
                    user.NewPassword = txtNewPassword.Text;
                    user.Role = rbtnAdmin.Checked ? "Admin" : rbtnAuthor.Checked ? "Author" : rbtnReader.Checked ? "Reader" : string.Empty;
                    System.Console.WriteLine(user.Role);
                    userService.UpdateUser(user);
                    MessageBox.Show("Cập nhật thành công!");
                }
                else if (UserSession.Instance.UserId != Guid.Empty && (UserSession.Instance.Role == "Author" || UserSession.Instance.Role == "Reader"))
                {
                    // Cập nhật cho user hiện tại (Author/Reader)
                    user.UserId = UserSession.Instance.UserId;
                    user.NewPassword = txtNewPassword.Text;
                    user.Role = rbtnAdmin.Checked ? "Admin" : rbtnAuthor.Checked ? "Author" : rbtnReader.Checked ? "Reader" : string.Empty;
                    userService.UpdateUser(user);
                    MessageBox.Show("Cập nhật thông tin cá nhân thành công!");
                }
                else
                {
                    user.Password = txtPassword.Text;
                    System.Console.WriteLine(txtPassword.Text);
                    user.Role = rbtnAdmin.Checked ? "Admin" : rbtnAuthor.Checked ? "Author" : rbtnReader.Checked ? "Reader" : string.Empty;
                    userService.AddUser(user);
                    MessageBox.Show("Thêm user mới thành công!");
                }
                if (_loadDGV != null)
                {
                    _loadDGV(GetRecords());
                }
                this.Close();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var dbValues = entry.GetDatabaseValues();

                if (dbValues == null)
                {
                    MessageBox.Show("Lỗi: User đã bị xóa khỏi hệ thống!");
                }
                else
                {
                    MessageBox.Show("Lỗi: Dữ liệu đã bị thay đổi bởi người khác. Vui lòng tải lại trang.");
                }
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show($"Lỗi CSDL: {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }
    }
}
