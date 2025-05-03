using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure;
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

namespace WindowsFormsApp1.Presentation
{
    public partial class FormPostViewingAdmin: Form
    {
        public new delegate void Load(DataTable li);
        private Load _loadDGV;
        private Guid _postId { get; set; }
        public FormPostViewingAdmin(Guid postId, Load LoadDGV)
        {
            InitializeComponent();
            _postId = postId;
            _loadDGV = LoadDGV;
            LoadGUI(_postId);
        }
        private void LoadGUI(Guid postId)
        {
            var postData = new PostService(new PostRepository(new ApplicationDbContext())).ShowPostViewingUser(postId);
            txtPostTitle.Text = postData.Title.ToString();
            txtCategory.Text = postData.Category.ToString();
            txtAuthor.Text = postData.AuthorName.ToString();
            txtStatus.Text = postData.Status.ToString();
            rTxtContent.Text = postData.Content.ToString();
            txtResponse.Text = postData.Response.ToString();
            dgvCmt.DataSource = postData.Comments.ToArray();
            if (txtStatus.Text.Equals("Pending"))
            {
                txtResponse.Enabled = true;
                btnAcp.Enabled = true;
                btnCancel.Enabled = true;
                btnCmt.Enabled = false;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
            }
            if(txtStatus.Text.Equals("Rejected"))
            {
                btnCmt.Enabled = false;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
            }
            dgvCmt.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCmt.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvCmt.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }
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
                dataTable.Rows.Add(Guid.Empty, string.Empty, string.Empty, string.Empty, DateTime.MinValue, 0, string.Empty);
            }
            return dataTable;
        }
        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (_loadDGV != null)
            {
                _loadDGV(GetRecords());
            }
            this.Close();
        }

        private void btnAcp_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtResponse.Text))
                {
                    MessageBox.Show("Không được để trống");
                    return;
                }

                new PostService(new PostRepository(new ApplicationDbContext())).AcceptPost(_postId, txtResponse.Text);
                MessageBox.Show("Thực hiện lưu vào cơ sở dữ liệu thành công");
                if (_loadDGV != null)
                {
                    _loadDGV(GetRecords());
                }
                this.Close();
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show("Lỗi thực hiện: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thực hiện: " + ex.Message);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtResponse.Text))
            {
                MessageBox.Show("Không được để trống");
                return;
            }
            new PostService(new PostRepository(new ApplicationDbContext())).RejectPost(_postId, txtResponse.Text);
            if (_loadDGV != null)
            {
                _loadDGV(GetRecords());
            }
            this.Close();
        }

        private void btnCmt_Click(object sender, EventArgs e)
        {
            FormComment formComment = new FormComment(Guid.Empty, _postId, LoadDGV);
            formComment.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvCmt.SelectedRows.Count == 1)
            {
                // Lấy commentId từ dòng được chọn
                Guid commentId = (Guid)dgvCmt.SelectedRows[0].Cells["CommentId"].Value;

                // Mở form chỉnh sửa và truyền commentId vào
                FormComment formComment = new FormComment(commentId,_postId, LoadDGV);
                formComment.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một bình luận để chỉnh sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void LoadDGV(DataTable li)
        {
            dgvCmt.DataSource = li;
        }
        
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCmt.SelectedRows.Count == 1)
            {
                // Lấy commentId từ dòng được chọn
                Guid commentId = (Guid)dgvCmt.SelectedRows[0].Cells["CommentId"].Value;
                Console.WriteLine(commentId);

                // Hỏi xác nhận trước khi xóa
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa bình luận này không?",
                                                      "Xác nhận xóa",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Xóa comment được chọn
                    new CommentService(new CommentRepository(new ApplicationDbContext())).DeleteComment(commentId);

                    // Cập nhật lại DataGridView
                    dgvCmt.DataSource = new PostService(new PostRepository(new ApplicationDbContext())).ShowPostViewingUser(_postId).Comments;
                    if (_loadDGV != null)
                    {
                        _loadDGV(GetRecords());
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một bình luận để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void dgvCmt_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCmt.SelectedRows.Count == 1)
            {
                try
                {
                    Guid currentUserId = (Guid)dgvCmt.SelectedRows[0].Cells["UserId"].Value;
                    Console.WriteLine("User của dòng: " + currentUserId);
                    Console.WriteLine("User đang đăng nhập: " + UserSession.Instance.UserId);

                    if (UserSession.Instance.UserId == currentUserId)
                    {
                        btnEdit.Enabled = true;
                        btnDelete.Enabled = true;
                    }
                    else
                    {
                        btnEdit.Enabled = false;
                        btnDelete.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi đọc UserId từ dòng được chọn: " + ex.Message);
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                }
            }
            else
            {
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
            }
        }
    }
}
