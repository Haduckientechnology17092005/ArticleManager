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
    public partial class FormPostManagement: Form
    {
        //private readonly PostService _postService;
        public FormPostManagement()
        {
            InitializeComponent();
            //var dbContext = new ApplicationDbContext();
            //_postService = new PostService(new PostRepository(dbContext));
            LoadComboBoxCategory();
            LoadComboBoxPostStatus();
            LoadDataGridViewPost();
            LoadGUI();
            dgvPost.SelectionChanged += DgvPost_SelectionChanged;
        }
        private void LoadGUI()
        {
            if(UserSession.Instance.Role == "Admin")
            {
                btnAdd.Enabled = false;
                btnEdit.Enabled = false;
            }
            else if (UserSession.Instance.Role == "Author")
            {
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
            }
            dgvPost.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPost.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvPost.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }
        private void LoadDataGridViewPost()
        {
            try
            {
                var currentUserId = UserSession.Instance.UserId;
                var currentUserRole = UserSession.Instance.Role;
                var postService = new PostService(new PostRepository(new ApplicationDbContext()));
                // Lọc bài viết theo quyền
                var postData = new List<PostManagerDTO>();
                if (currentUserRole == "Author")
                {
                    postData = postService.SearchByAuthor(currentUserId, "All", "All", null);
                }
                else
                {
                    postData = postService.SearchByAdmin("All", "All", null);
                }
                dgvPost.DataSource = postData;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu bài viết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadComboBoxCategory()
        {
            var categoryService = new CategoryService(new CategoryRepository(new ApplicationDbContext()));
            var categories = categoryService.GetAllCategoriesName();
            cBBCategory.Items.Clear();
            cBBCategory.Items.Add("All");
            foreach (var category in categories)
            {
                cBBCategory.Items.Add(category);
            }
            cBBCategory.SelectedItem = cBBCategory.Items[0];
        }
        private void LoadComboBoxPostStatus()
        {
            var postService = new PostService(new PostRepository(new ApplicationDbContext()));
            var statuses = postService.GetAllPostStatus();
            cBBStatus.Items.Clear();
            cBBStatus.Items.Add("All");
            foreach (var status in statuses)
            {
                cBBStatus.Items.Add(status);
            }
            cBBStatus.SelectedItem = cBBStatus.Items[0];
        }
        private void btnReturn_Click(object sender, EventArgs e)
        {
            try
            {
                if(UserSession.Instance.Role == "Admin")
                {
                    FormAdmin formAdmin = new FormAdmin();
                    formAdmin.Show();
                    this.Close();
                }
                else if (UserSession.Instance.Role == "Author")
                {
                    FormAuthor formAuthor = new FormAuthor();
                    formAuthor.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("You do not have permission to access this feature.");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("An error occurred while returning to the previous form: " + ex.Message);
            }
        }
        private void DgvPost_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPost.SelectedRows.Count == 0)
            {
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                return;
            }

            var selectedRow = dgvPost.SelectedRows[0];
            var selectedCell = selectedRow.Cells["PostId"];
            var selectedCellStatus = selectedRow.Cells["Status"];

            if (selectedCell?.Value != null && Guid.TryParse(selectedCell.Value.ToString(), out Guid postId))
            {
                string status = selectedCellStatus?.Value?.ToString();

                if (UserSession.Instance?.Role == "Author" && status == "Pending")
                {
                    var post = new PostService(new PostRepository(new ApplicationDbContext())).GetPostById(postId);
                    //var post = _postService.GetPostById(postId);
                    bool isOwner = post?.UserId == UserSession.Instance.UserId;

                    btnEdit.Enabled = isOwner;
                    btnDelete.Enabled = isOwner;
                    return;
                }

                if (UserSession.Instance?.Role == "Author" && status == "Approved")
                {
                    var post = new PostService(new PostRepository(new ApplicationDbContext())).GetPostById(postId);
                    //var post = _postService.GetPostById(postId);
                    bool isOwner = post?.UserId == UserSession.Instance.UserId;

                    btnEdit.Enabled = false;
                    btnDelete.Enabled = isOwner;
                    return;
                }

                if (UserSession.Instance?.Role == "Admin")
                {
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = true;
                    return;
                }
            }

            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                //if (UserSession.Instance.Role == "Author")
                //{
                //    var authorId = UserSession.Instance.UserId;
                //    //var postData = new PostService(new PostRepository(new ApplicationDbContext())).SearchByAuthor(authorId, cBBCategory.Text, cBBStatus.Text, txtSearch.Text);
                //    var postData = _postService.SearchByAuthor(authorId, cBBCategory.Text, cBBStatus.Text, txtSearch.Text); 
                //    System.Console.WriteLine(authorId + cBBCategory.Text + cBBStatus.Text + txtSearch.Text);
                //    dgvPost.DataSource = postData;
                //}
                //else
                //{
                //    var postData = new PostService(new PostRepository(new ApplicationDbContext())).SearchByAdmin(cBBCategory.Text, cBBStatus.Text, txtSearch.Text);
                //    dgvPost.DataSource = postData;
                //}
                //cBBSort.SelectedIndex = 1;
                LoadPostData();
                cBBSort.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnViewPost_Click(object sender, EventArgs e)
        {
            if (dgvPost.SelectedRows.Count == 1)
            {
                Guid ID = Guid.Parse(dgvPost.SelectedRows[0].Cells["PostId"].Value.ToString());
                if(UserSession.Instance.Role.Equals("Author"))
                {
                    FormPostViewingUser details = new FormPostViewingUser(ID, LoadDGV);
                    details.ShowDialog();
                }
                else
                {
                    FormPostViewingAdmin details = new FormPostViewingAdmin(ID, LoadDGV);
                    details.ShowDialog();
                }    
            }
            else
            {
                MessageBox.Show("Chỉ được chọn 1 post để xem");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPost.SelectedRows.Count != 1)
                {
                    MessageBox.Show("Vui lòng chọn 1 bài báo để xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                Guid postId = Guid.Parse(dgvPost.SelectedRows[0].Cells["PostId"].Value.ToString());
                DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa bài báo này?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2); // Mặc định chọn No
                if (result == DialogResult.Yes)
                {
                    var postService = new PostService(new PostRepository(new ApplicationDbContext()));
                    postService.SoftDeletePost(postId);
                    MessageBox.Show("Xóa bài báo thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (UserSession.Instance.Role.Equals("Author"))
                    {
                        var postData = postService.SearchByAuthor(UserSession.Instance.UserId, cBBCategory.Text, cBBStatus.Text, txtSearch.Text);
                        dgvPost.DataSource = postData;
                    }
                    if (UserSession.Instance.Role.Equals("Admin"))
                    {
                        var postData = postService.SearchByAdmin(cBBCategory.Text, cBBStatus.Text, txtSearch.Text);
                        dgvPost.DataSource = postData;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FormCreateEditPost formCreateEditPost = new FormCreateEditPost(Guid.Empty, LoadDGV);
            formCreateEditPost.ShowDialog();
        }
        public void LoadDGV(DataTable li)
        {
            dgvPost.DataSource = li;
            cBBCategory.SelectedIndex = 0;
            cBBStatus.SelectedIndex = 0;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvPost.SelectedRows.Count == 1)
            {
                Guid ID = Guid.Parse(dgvPost.SelectedRows[0].Cells["PostId"].Value.ToString());
                FormCreateEditPost editForm = new FormCreateEditPost(ID, LoadDGV);
                editForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Chỉ được chọn 1 user để chính sửa");
            }
        }

        private void dgvPost_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            try
            {
                //if(cBBSort.Text == "")
                //{
                //    if (UserSession.Instance.Role == "Author")
                //    {
                //        var authorId = UserSession.Instance.UserId;
                //        //var postData = new PostService(new PostRepository(new ApplicationDbContext())).SearchByAuthor(authorId, cBBCategory.Text, cBBStatus.Text, txtSearch.Text);
                //        var postData = _postService.SearchByAuthor(authorId, cBBCategory.Text, cBBStatus.Text, txtSearch.Text);
                //        postData = postData.OrderByDescending(p => p.CreatedAt).ToList();
                //        System.Console.WriteLine(authorId + cBBCategory.Text + cBBStatus.Text + txtSearch.Text);
                //        dgvPost.DataSource = postData;
                //    }
                //    else
                //    {
                //        //var postData = new PostService(new PostRepository(new ApplicationDbContext())).SearchByAdmin(cBBCategory.Text, cBBStatus.Text, txtSearch.Text);
                //        var postData = _postService.SearchByAdmin(cBBCategory.Text, cBBStatus.Text, txtSearch.Text);
                //        postData = postData.OrderByDescending(p => p.CreatedAt).ToList();
                //        dgvPost.DataSource = postData;
                //    }
                //} else if (cBBSort.Text == "Thời gian từ hiện tại về trước")
                //{
                //    if (UserSession.Instance.Role == "Author")
                //    {
                //        var authorId = UserSession.Instance.UserId;
                //        //var postData = new PostService(new PostRepository(new ApplicationDbContext())).SearchByAuthor(authorId, cBBCategory.Text, cBBStatus.Text, txtSearch.Text);
                //        var postData = _postService.SearchByAuthor(authorId, cBBCategory.Text, cBBStatus.Text, txtSearch.Text); 
                //        postData = postData.OrderByDescending(p => p.CreatedAt).ToList();
                //        System.Console.WriteLine(authorId + cBBCategory.Text + cBBStatus.Text + txtSearch.Text);
                //        dgvPost.DataSource = postData;
                //    }
                //    else
                //    {
                //        //var postData = new PostService(new PostRepository(new ApplicationDbContext())).SearchByAdmin(cBBCategory.Text, cBBStatus.Text, txtSearch.Text);
                //        var postData = _postService.SearchByAdmin(cBBCategory.Text, cBBStatus.Text, txtSearch.Text);
                //        postData = postData.OrderByDescending(p => p.CreatedAt).ToList();
                //        dgvPost.DataSource = postData;
                //    }
                //} else if (cBBSort.Text == "Thời gian từ trước đến hiện tại")
                //{
                //    if (UserSession.Instance.Role == "Author")
                //    {
                //        var authorId = UserSession.Instance.UserId;
                //        //var postData = new PostService(new PostRepository(new ApplicationDbContext())).SearchByAuthor(authorId, cBBCategory.Text, cBBStatus.Text, txtSearch.Text);
                //        var postData = _postService.SearchByAuthor(authorId, cBBCategory.Text, cBBStatus.Text, txtSearch.Text);
                //        postData = postData.OrderBy(p => p.CreatedAt).ToList();
                //        System.Console.WriteLine(authorId + cBBCategory.Text + cBBStatus.Text + txtSearch.Text);
                //        dgvPost.DataSource = postData;
                //    }
                //    else
                //    {
                //        //var postData = new PostService(new PostRepository(new ApplicationDbContext())).SearchByAdmin(cBBCategory.Text, cBBStatus.Text, txtSearch.Text);
                //        var postData = _postService.SearchByAdmin(cBBCategory.Text, cBBStatus.Text, txtSearch.Text);
                //        postData = postData.OrderBy(p => p.CreatedAt).ToList();
                //        dgvPost.DataSource = postData;
                //    }
                //}
                LoadPostData(applySort: true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void LoadPostData(bool applySort = false)
        {
            try
            {
                var category = cBBCategory.Text;
                var status = cBBStatus.Text;
                var keyword = txtSearch.Text;
                var role = UserSession.Instance.Role;
                var userId = UserSession.Instance.UserId;

                List<PostManagerDTO> postData;
                PostService postService = new PostService(new PostRepository(new ApplicationDbContext()));

                if (role == "Author")
                {
                    postData = postService.SearchByAuthor(userId, category, status, keyword);
                }
                else
                {
                    postData = postService.SearchByAdmin(category, status, keyword);
                }

                // Apply sort if needed
                string sortOption = cBBSort.Text.Trim();
                if (applySort && !string.IsNullOrEmpty(sortOption))
                {
                    if (sortOption == "Thời gian từ hiện tại về trước")
                    {
                        postData = postData.OrderByDescending(p => p.CreatedAt).ToList();
                    }
                    else if (sortOption == "Thời gian từ trước đến hiện tại")
                    {
                        postData = postData.OrderBy(p => p.CreatedAt).ToList();
                    }
                }

                dgvPost.DataSource = postData;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu bài viết: " + ex.Message);
            }
        }
    }
}
