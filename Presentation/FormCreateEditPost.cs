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
using WindowsFormsApp1.DAL.Repository;
using WindowsFormsApp1.DAL.Models;
using WindowsFormsApp1.Session;
using System.Data.Entity.Infrastructure;
using WindowsFormsApp1.DTOs;
namespace WindowsFormsApp1.Presentation
{
    public partial class FormCreateEditPost: Form
    {
        public new delegate void Load(DataTable li);
        private Load _loadDGV;
        private Guid _postId { get; set; }
        public FormCreateEditPost(Guid PostId, Load LoadDGV)
        {
            InitializeComponent();
            LoadComboBox();
            _postId = PostId;
            _loadDGV = LoadDGV;
            LoadGUI();
        }
        private void LoadComboBox()
        {
            cBBCategory.Items.Clear();
            var categories = new CategoryService(new CategoryRepository(new ApplicationDbContext())).GetAllCategories();
            cBBCategory.DataSource = categories;
            cBBCategory.DisplayMember = "Name";
            cBBCategory.ValueMember = "CategoryId";
            cBBCategory.SelectedItem = 0;
        }
        public void LoadGUI()
        {
            //kiem tra null
            if(_postId == Guid.Empty)
            {
                txtPostTitle.Text = string.Empty;
                cBBCategory.SelectedIndex = 0;
                txtStatus.Text = string.Empty;
                rTxtContent.Text = string.Empty;
            }
            else
            {
                LoadGUI(_postId);
            }   
        }
        public void LoadGUI(Guid _postId)
        {
            try
            {
                Guid authorId = UserSession.Instance.UserId;
                var postService = new PostService(new PostRepository(new ApplicationDbContext()));
                var postData = postService.ShowPostViewingUser(_postId);
                txtPostTitle.Text = postData.Title;
                cBBCategory.SelectedItem = postData.Category;
                txtStatus.Text = postData.Status;
                rTxtContent.Text = postData.Content;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var dbValues = entry.GetDatabaseValues();

                if (dbValues == null)
                {
                    MessageBox.Show("Lỗi: Post đã bị xóa khỏi hệ thống!");
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
        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cBBCategory_SelectedIndexChanged_1(object sender, EventArgs e){}
        public DataTable GetRecords()
        {
            var currentUserId = UserSession.Instance.UserId;
            var currentUserRole = UserSession.Instance.Role;
            var postService = new PostService(new PostRepository(new ApplicationDbContext()));
            // Lọc bài viết theo quyền
            var postDatas = new List<PostManagerDTO>();
            if (currentUserRole == "Author")
            {
                postDatas = postService.SearchByAuthor(currentUserId, "All", "All", null);
            }
            else
            {
                postDatas = postService.SearchByAdmin("All", "All", null);
            }
            // Create a DataTable to store the data
            var dataTable = new DataTable();
            dataTable.Columns.Add("PostId", typeof(Guid));
            dataTable.Columns.Add("PostName", typeof(string));
            dataTable.Columns.Add("CategoryName", typeof(string));
            dataTable.Columns.Add("AuthorName", typeof(string));
            dataTable.Columns.Add("CreateAt", typeof(DateTime));
            dataTable.Columns.Add("CountComment", typeof(int));
            dataTable.Columns.Add("Status", typeof(string));
            //fix was null
            foreach (var post in postDatas)
            {
                dataTable.Rows.Add(post.PostId, post.PostName, post.CategoryName, post.AuthorName, post.CreatedAt, post.CountComment, post.Status);
            }
            if (dataTable.Rows.Count == 0)
            {
                dataTable.Rows.Add(Guid.Empty, string.Empty, string.Empty, 0, string.Empty, string.Empty, DateTime.MinValue);
            }
            return dataTable;
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                var post = new PostDTO
                {
                    Title = txtPostTitle.Text,
                    Content = rTxtContent.Text,
                    CategoryId = (cBBCategory.SelectedItem as Category).CategoryId,
                    UserId = UserSession.Instance.UserId
                };
                if (_postId == Guid.Empty)
                {
                    post.PostId = Guid.NewGuid();
                    new PostService(new PostRepository(new ApplicationDbContext())).CreatePost(post);
                }
                else
                {
                    post.PostId = _postId;
                    new PostService(new PostRepository(new ApplicationDbContext())).UpdatePost(post);
                }
                MessageBox.Show("Thực hiện lưu vào cơ sở dữ liệu thành công");
                if (_loadDGV != null)
                {
                    _loadDGV(GetRecords());
                }
                this.Close();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Lỗi thực hiện" + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi thực hiện" + ex.Message);
            }
        }
    }
}
