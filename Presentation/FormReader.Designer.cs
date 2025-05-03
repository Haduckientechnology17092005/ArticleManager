namespace WindowsFormsApp1.Presentation
{
    partial class FormReader
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
            this.dgvPost = new System.Windows.Forms.DataGridView();
            this.btnProfilePost = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.cBBCategory = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.btnProfile = new System.Windows.Forms.Button();
            this.btnEditProfile = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPost)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPost
            // 
            this.dgvPost.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPost.Location = new System.Drawing.Point(22, 100);
            this.dgvPost.Name = "dgvPost";
            this.dgvPost.Size = new System.Drawing.Size(749, 303);
            this.dgvPost.TabIndex = 0;
            // 
            // btnProfilePost
            // 
            this.btnProfilePost.Location = new System.Drawing.Point(22, 72);
            this.btnProfilePost.Name = "btnProfilePost";
            this.btnProfilePost.Size = new System.Drawing.Size(116, 23);
            this.btnProfilePost.TabIndex = 1;
            this.btnProfilePost.Text = "Xem chi tiết bài báo";
            this.btnProfilePost.UseVisualStyleBackColor = true;
            this.btnProfilePost.Click += new System.EventHandler(this.btnProfilePost_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(696, 71);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(555, 74);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(135, 20);
            this.txtSearch.TabIndex = 3;
            // 
            // cBBCategory
            // 
            this.cBBCategory.FormattingEnabled = true;
            this.cBBCategory.Location = new System.Drawing.Point(428, 74);
            this.cBBCategory.Name = "cBBCategory";
            this.cBBCategory.Size = new System.Drawing.Size(121, 21);
            this.cBBCategory.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(377, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Thể loại";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(277, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(208, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Chào mừng bạn đến trang chủ người dùng";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(696, 415);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "Đăng xuất";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnProfile
            // 
            this.btnProfile.Location = new System.Drawing.Point(22, 415);
            this.btnProfile.Name = "btnProfile";
            this.btnProfile.Size = new System.Drawing.Size(132, 23);
            this.btnProfile.TabIndex = 10;
            this.btnProfile.Text = "Xem thông tin cá nhân";
            this.btnProfile.UseVisualStyleBackColor = true;
            this.btnProfile.Click += new System.EventHandler(this.btnProfile_Click);
            // 
            // btnEditProfile
            // 
            this.btnEditProfile.Location = new System.Drawing.Point(178, 415);
            this.btnEditProfile.Name = "btnEditProfile";
            this.btnEditProfile.Size = new System.Drawing.Size(132, 23);
            this.btnEditProfile.TabIndex = 11;
            this.btnEditProfile.Text = "Sửa thông tin cá nhân";
            this.btnEditProfile.UseVisualStyleBackColor = true;
            this.btnEditProfile.Click += new System.EventHandler(this.btnEditProfile_Click);
            // 
            // FormReader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 450);
            this.Controls.Add(this.btnEditProfile);
            this.Controls.Add(this.btnProfile);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cBBCategory);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnProfilePost);
            this.Controls.Add(this.dgvPost);
            this.Name = "FormReader";
            this.Text = "FormReader";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPost)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPost;
        private System.Windows.Forms.Button btnProfilePost;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cBBCategory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnProfile;
        private System.Windows.Forms.Button btnEditProfile;
    }
}