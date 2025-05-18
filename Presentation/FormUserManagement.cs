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
            var userData = userService.MapUsersToUserWithPostCountDTO(users, posts);
            userData = userData.OrderByDescending(p => p.CreatedAt).ToList();
            dgvUser.DataSource = null;
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
                var userData = FilterAndSortUsers();
                dgvUser.DataSource = userData;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message);
            }
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
                //Admin được sửa chính mình
                Guid ID = Guid.Parse(dgvUser.SelectedRows[0].Cells["UserId"].Value.ToString());
                //Admin không được sửa admin
                String role = dgvUser.SelectedRows[0].Cells["Role"].Value.ToString();
                if (role == "Admin" && UserSession.Instance.UserId != ID)
                {
                    MessageBox.Show("Không thể sửa người dùng này vì họ là Admin", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
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
            //Admin không được xóa admin
            if (userId == UserSession.Instance.UserId)
            {
                MessageBox.Show("Không thể xóa chính mình", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            String role = dgvUser.SelectedRows[0].Cells["Role"].Value.ToString();
            if (role == "Admin")
            {
                MessageBox.Show("Không thể xóa người dùng này vì họ là Admin", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
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
                MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                var userService = new UserService(new UserRepository(new ApplicationDbContext()));
                userService.DeleteUser(userId);
                MessageBox.Show("Xóa người dùng thành công!", "Thành công",MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Cập nhật lại DataGridView
                LoadDataGridViewUser();
            }
        }
        public void LoadDGV(DataTable li)
        {
            dgvUser.DataSource = li;
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            try
            {
                var userData = FilterAndSortUsers();
                dgvUser.DataSource = userData;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sắp xếp: " + ex.Message);
            }
        }
        private List<UserWithPostCountDTO> FilterAndSortUsers()
        {
            var userService = new UserService(new UserRepository(new ApplicationDbContext()));
            var postService = new PostService(new PostRepository(new ApplicationDbContext()));

            var posts = postService.GetAllPosts();
            
            List<User> users = new List<User>();
            
            string searchText = textSearch.Text.Trim();
            string selectedRole = cBBRole.SelectedItem?.ToString() ?? "All";

            if (string.IsNullOrEmpty(searchText) && selectedRole == "All")
            {
                users = userService.GetAllUsers();
            }
            else if (string.IsNullOrEmpty(searchText))
            {
                users = userService.GetListUserByRole(selectedRole);
            }
            else if (selectedRole == "All")
            {
                users = userService.GetListUserByUserName(searchText);
            }
            else
            {
                users = userService.GetListUserByRoleAndUserName(selectedRole, searchText);
            }

            var userData = userService.MapUsersToUserWithPostCountDTO(users, posts);

            // Sắp xếp theo lựa chọn
            switch (cBBSort.SelectedIndex)
            {
                case 0: // Mặc định hoặc từ mới đến cũ
                case -1:
                    userData = userData.OrderByDescending(u => u.CreatedAt).ToList();
                    break;
                case 1: // Từ cũ đến mới
                    userData = userData.OrderBy(u => u.CreatedAt).ToList();
                    break;
                case 2: // Số bài viết giảm dần
                    userData = userData.OrderByDescending(u => u.PostCount).ToList();
                    break;
                case 3: // Số bài viết tăng dần
                    userData = userData.OrderBy(u => u.PostCount).ToList();
                    break;
            }

            return userData;
        }

        //private List<UserWithPostCountDTO> SortUsers(List<UserWithPostCountDTO> users)
        //{
        //    if (cBBSort.Text == "")
        //    {
        //        return users.OrderBy(u => u.CreatedAt).ToList();
        //    }
        //    else if (cBBSort.SelectedIndex == 0)
        //    {
        //        return users.OrderByDescending(u => u.CreatedAt).ToList();
        //    }
        //    else if (cBBSort.SelectedIndex == 1)
        //    {
        //        return users.OrderBy(u => u.CreatedAt).ToList();
        //    }
        //    else if (cBBSort.SelectedIndex == 2)
        //    {
        //        return users.OrderByDescending(u => u.PostCount).ToList();
        //    }
        //    else if (cBBSort.SelectedIndex == 3)
        //    {
        //        return users.OrderBy(u => u.PostCount).ToList();
        //    }
        //    return users;
        //}

        private void cBBSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSearch_Click(sender, e);
        }

        private void cBBRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSearch_Click(sender, e);
        }
    }
}
