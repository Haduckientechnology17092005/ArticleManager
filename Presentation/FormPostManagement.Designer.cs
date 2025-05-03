namespace WindowsFormsApp1.Presentation
{
    partial class FormPostManagement
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.dgvPost = new System.Windows.Forms.DataGridView();
            this.cBBCategory = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnViewPost = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnSort = new System.Windows.Forms.Button();
            this.cBBSort = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cBBStatus = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPost)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(326, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Quản lý bài báo khoa học";
            // 
            // dgvPost
            // 
            this.dgvPost.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPost.Location = new System.Drawing.Point(22, 90);
            this.dgvPost.Name = "dgvPost";
            this.dgvPost.Size = new System.Drawing.Size(755, 316);
            this.dgvPost.TabIndex = 1;
            this.dgvPost.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPost_CellContentClick);
            // 
            // cBBCategory
            // 
            this.cBBCategory.FormattingEnabled = true;
            this.cBBCategory.Location = new System.Drawing.Point(70, 64);
            this.cBBCategory.Name = "cBBCategory";
            this.cBBCategory.Size = new System.Drawing.Size(121, 21);
            this.cBBCategory.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Thể loại";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(702, 61);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(535, 64);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(161, 20);
            this.txtSearch.TabIndex = 5;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(22, 415);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(103, 415);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 7;
            this.btnEdit.Text = "Sửa";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(184, 415);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnViewPost
            // 
            this.btnViewPost.Location = new System.Drawing.Point(265, 415);
            this.btnViewPost.Name = "btnViewPost";
            this.btnViewPost.Size = new System.Drawing.Size(75, 23);
            this.btnViewPost.TabIndex = 9;
            this.btnViewPost.Text = "Xem";
            this.btnViewPost.UseVisualStyleBackColor = true;
            this.btnViewPost.Click += new System.EventHandler(this.btnViewPost_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.Location = new System.Drawing.Point(346, 415);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(75, 23);
            this.btnReturn.TabIndex = 10;
            this.btnReturn.Text = "Quay lại";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnSort
            // 
            this.btnSort.Location = new System.Drawing.Point(575, 415);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(75, 23);
            this.btnSort.TabIndex = 12;
            this.btnSort.Text = "Sắp xếp";
            this.btnSort.UseVisualStyleBackColor = true;
            // 
            // cBBSort
            // 
            this.cBBSort.FormattingEnabled = true;
            this.cBBSort.Items.AddRange(new object[] {
            "Bình luận từ cao đến thấp",
            "Bình luận từ thấp đến cao",
            "Thời gian từ trước đến hiện tại",
            "Thời gian từ hiện tại về trước"});
            this.cBBSort.Location = new System.Drawing.Point(656, 417);
            this.cBBSort.Name = "cBBSort";
            this.cBBSort.Size = new System.Drawing.Size(121, 21);
            this.cBBSort.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(211, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Trạng thái";
            // 
            // cBBStatus
            // 
            this.cBBStatus.FormattingEnabled = true;
            this.cBBStatus.Location = new System.Drawing.Point(272, 64);
            this.cBBStatus.Name = "cBBStatus";
            this.cBBStatus.Size = new System.Drawing.Size(121, 21);
            this.cBBStatus.TabIndex = 15;
            // 
            // FormPostManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cBBStatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cBBSort);
            this.Controls.Add(this.btnSort);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.btnViewPost);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cBBCategory);
            this.Controls.Add(this.dgvPost);
            this.Controls.Add(this.label1);
            this.Name = "FormPostManagement";
            this.Text = "FormPostManagement";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPost)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvPost;
        private System.Windows.Forms.ComboBox cBBCategory;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnViewPost;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnSort;
        private System.Windows.Forms.ComboBox cBBSort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cBBStatus;
    }
}