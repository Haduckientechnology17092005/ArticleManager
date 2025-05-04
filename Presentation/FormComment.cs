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

namespace WindowsFormsApp1.Presentation
{
    public partial class FormComment: Form
    {
        private Guid _commentId { get; set; }
        private Guid _postId { get; set; }
        public new delegate void Load(DataTable li);
        private Load _loadDGV;
        public FormComment(Guid commentId, Guid postId, Load loadDGV)
        {
            InitializeComponent();
            _commentId = commentId;
            _loadDGV = loadDGV;
            _postId = postId;
            LoadGUI();
        }
        public void LoadGUI()
        {
            try
            {
                if (_commentId == Guid.Empty)
                {
                    txtComment.Text = string.Empty;
                }
                else
                {
                    LoadGUI(_commentId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        public void LoadGUI(Guid commentId)
        {
            try
            {
                var comment = new CommentService(new CommentRepository(new ApplicationDbContext())).FindCommentById(commentId);
                if (comment != null)
                {
                    txtComment.Text = comment.Content.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (_commentId == Guid.Empty)
                {
                    var comment = new Comment();
                    comment.Content = txtComment.Text;
                    comment.UserId = UserSession.Instance.UserId;
                    comment.PostId = _postId;
                    new CommentService(new CommentRepository(new ApplicationDbContext())).AddComment(comment);
                }
                else
                {
                    var comment = new Comment();
                    comment.CommentId = _commentId;
                    comment.Content = txtComment.Text;
                    comment.PostId = _postId;
                    comment.UserId = UserSession.Instance.UserId;
                    new CommentService(new CommentRepository(new ApplicationDbContext())).UpdateComment(comment);
                }
                if (_loadDGV != null)
                {
                    _loadDGV(GetRecordsComment());
                }
                this.Close();
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        public DataTable GetRecordsComment()
        {
            try
            {
                var currentUserId = UserSession.Instance.UserId;
                var postData = new PostService(new PostRepository(new ApplicationDbContext())).ShowPostViewingUser(_postId);
                var commentService = new CommentService(new CommentRepository(new ApplicationDbContext()));
                var commentsData = postData.Comments.ToList();
                // Create a DataTable to store the data
                var dataTable = new DataTable();
                dataTable.Columns.Add("CommentId", typeof(Guid));
                dataTable.Columns.Add("Content", typeof(string));
                dataTable.Columns.Add("PostId", typeof(Guid));
                dataTable.Columns.Add("UserId", typeof(Guid));
                dataTable.Columns.Add("UserName", typeof(string));
                dataTable.Columns.Add("CreateAt", typeof(DateTime));
                //fix was null
                foreach (var comment in commentsData)
                {
                    dataTable.Rows.Add(comment.CommentId, comment.Content, comment.PostId, comment.UserId, comment.UserName, comment.CreatedAt);
                }
                if (dataTable.Rows.Count == 0)
                {
                    dataTable.Rows.Add(Guid.Empty, string.Empty, Guid.Empty, Guid.Empty, DateTime.MinValue);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
