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
using WindowsFormsApp1.Session;
namespace WindowsFormsApp1.Presentation
{
    public partial class FormReader: Form
    {
        public FormReader()
        {
            InitializeComponent();
            LoadComboBoxCategory();
            LoadDataGridViewPost();

        }
        private void LoadComboBoxCategory()
        {
            cBBCategory.Items.Clear();
            cBBCategory.Items.Add("All");
            var categories = new CategoryService(new CategoryRepository(new ApplicationDbContext())).GetAllCategoriesName();
            foreach (var category in categories)
            {
                cBBCategory.Items.Add($"{category}");
            }
            cBBCategory.SelectedIndex = 0;
        }
        private void LoadDataGridViewPost()
        {
            var postData = new PostService(new PostRepository(new ApplicationDbContext())).SearchByUser("All",null);
            dgvPost.DataSource = postData;
            dgvPost.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPost.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvPost.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }
        public void LoadDGV(DataTable li)
        {
            dgvPost.DataSource = li;
            cBBCategory.SelectedIndex = 0;
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

        private void btnProfile_Click(object sender, EventArgs e)
        {
            FormUserDetails formUserDetails = new FormUserDetails();
            formUserDetails.ShowDialog();
            this.Hide();
        }

        private void btnEditProfile_Click(object sender, EventArgs e)
        {
            FormCreateEditUser formCreateEditUser = new FormCreateEditUser();
            formCreateEditUser.ShowDialog();
            System.Console.WriteLine($"User ID: {UserSession.Instance.UserId}");
            System.Console.WriteLine("Thanh cong !");
            formCreateEditUser.Text = "Edit Profile";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var postData = new PostService(new PostRepository(new ApplicationDbContext())).SearchByUser(cBBCategory.Text, txtSearch.Text);
            dgvPost.DataSource = postData;
        }

        private void btnProfilePost_Click(object sender, EventArgs e)
        {
            //chon 1 o trong datagridview
            if (dgvPost.SelectedRows.Count == 1)
            {
                Guid ID = Guid.Parse(dgvPost.SelectedRows[0].Cells["PostId"].Value.ToString());
                FormPostViewingUser details = new FormPostViewingUser(ID, LoadDGV);
                details.ShowDialog();
            }
            else
            {
                MessageBox.Show("Chỉ được chọn 1 post để xem");
            }
        }
    }
}
