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
    public partial class FormUserManagement: Form
    {
        public FormUserManagement()
        {
            InitializeComponent();
            LoadDataCBBRole();
            LoadDataGridViewUser();
        }
        private void LoadDataCBBRole()
        {
            //Load data from database
            var roles = new UserService(new UserRepository(new ApplicationDbContext())).GetAllRoles();
            //Load data into the combo box
            cBBRole.Items.Clear();
            cBBRole.Items.Add("All");
            foreach (var role in roles)
            {
                cBBRole.Items.Add(role);
            }
            cBBRole.SelectedItem = cBBRole.Items[0];
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            FormAdmin formAdmin = new FormAdmin();
            formAdmin.Show();
            this.Close();
        }
        private void LoadDataGridViewUser()
        {
            var userService = new UserService(new UserRepository(new ApplicationDbContext()));
            var postService = new PostService(new PostRepository(new ApplicationDbContext()));

            var users = userService.GetAllUsers();
            var posts = postService.GetAllPosts();

            // Tạo danh sách DTO để hiển thị
            var userData = MapUsersToUserWithPostCountDTO(users, posts);
            // Đổ dữ liệu vào DataGridView
            dgvUser.DataSource = userData;
            dgvUser.Columns["PostCount"].HeaderText = "Số bài đã đăng";
            dgvUser.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUser.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvUser.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                var userService = new UserService(new UserRepository(new ApplicationDbContext()));
                var postService = new PostService(new PostRepository(new ApplicationDbContext()));
                //Neu text rong va Role la All
                if (string.IsNullOrEmpty(textSearch.Text) && cBBRole.SelectedItem.ToString() == "All")
                {
                    LoadDataGridViewUser();
                    return;
                }
                //Neu text rong va Role khong phai All
                if (string.IsNullOrEmpty(textSearch.Text) && cBBRole.SelectedItem.ToString() != "All")
                {
                    var users = userService.GetListUserByRole(cBBRole.SelectedItem.ToString());
                    var posts = postService.GetAllPosts();
                    var userData = MapUsersToUserWithPostCountDTO(users, posts);
                    dgvUser.DataSource = userData;
                    return;
                }
                //Neu text khong rong va Role la All
                if (!string.IsNullOrEmpty(textSearch.Text) && cBBRole.SelectedItem.ToString() == "All")
                {
                    var users = userService.GetListUserByUserName(textSearch.Text);
                    var posts = postService.GetAllPosts();
                    var userData = MapUsersToUserWithPostCountDTO(users, posts);
                    dgvUser.DataSource = userData;
                    return;
                }
                //Neu text khong rong va Role khong phai All
                if (!string.IsNullOrEmpty(textSearch.Text) && cBBRole.SelectedItem.ToString() != "All")
                {
                    var users = userService.GetListUserByRoleAndUserName(cBBRole.SelectedItem.ToString(), textSearch.Text);
                    var posts = postService.GetAllPosts();
                    var userData = MapUsersToUserWithPostCountDTO(users, posts);
                    dgvUser.DataSource = userData;
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while searching: " + ex.Message);
            }
        }
        private List<UserWithPostCountDTO> MapUsersToUserWithPostCountDTO(IEnumerable<User> users, IEnumerable<Post> posts)
        {
            var userData = users.Select(u => new UserWithPostCountDTO
            {
                UserId = u.UserId,
                Username = u.Username,
                Email = u.Email,
                PostCount = posts.Count(p => p.UserId == u.UserId),
                Role = u.Role,
                Password = u.Password,
                CreatedAt = u.CreatedAt
            }).ToList();
            return userData;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int oldRowCOunt = dgvUser.Rows.Count;
            FormCreateEditUser addForm = new FormCreateEditUser(Guid.Empty, LoadDGV);
            addForm.ShowDialog();
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvUser.SelectedRows.Count == 1)
            {
                Guid ID = Guid.Parse(dgvUser.SelectedRows[0].Cells["UserId"].Value.ToString());
                FormCreateEditUser editForm = new FormCreateEditUser(ID, LoadDGV);
                editForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Chỉ được chọn 1 user để chính sửa");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Kiểm tra có dòng nào được chọn không
            if (dgvUser.SelectedRows.Count != 1)
            {
                MessageBox.Show("Vui lòng chọn 1 người dùng để xóa", "Thông báo",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Lấy ID người dùng được chọn
            Guid userId = Guid.Parse(dgvUser.SelectedRows[0].Cells["UserId"].Value.ToString());

            // Kiểm tra người dùng có bài viết không
            var postService = new PostService(new PostRepository(new ApplicationDbContext()));
            if (postService.GetAllPosts().Any(p => p.UserId == userId))
            {
                MessageBox.Show("Không thể xóa người dùng này vì họ đã đăng bài viết.","Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Hiển thị hộp thoại xác nhận
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa người dùng này?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2); // Mặc định chọn No
            if (result == DialogResult.Yes)
            {
                var userService = new UserService(new UserRepository(new ApplicationDbContext()));
                userService.DeleteUser(userId);
                MessageBox.Show("Xóa người dùng thành công!", "Thành công",MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataGridViewUser();
            }
        }
        public void LoadDGV(DataTable li)
        {
            dgvUser.DataSource = li;
        }
    }
}
