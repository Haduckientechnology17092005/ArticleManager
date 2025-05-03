namespace WindowsFormsApp1.Presentation
{
    partial class FormAuthor
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
            this.btnProfile = new System.Windows.Forms.Button();
            this.btnPost = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnEditProfile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(225, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Chào mừng bạn đến với trang dành cho Author";
            // 
            // btnProfile
            // 
            this.btnProfile.Location = new System.Drawing.Point(33, 103);
            this.btnProfile.Name = "btnProfile";
            this.btnProfile.Size = new System.Drawing.Size(202, 23);
            this.btnProfile.TabIndex = 1;
            this.btnProfile.Text = "Xem thông tin cá nhân";
            this.btnProfile.UseVisualStyleBackColor = true;
            this.btnProfile.Click += new System.EventHandler(this.buttonProfile_Click);
            // 
            // btnPost
            // 
            this.btnPost.Location = new System.Drawing.Point(253, 103);
            this.btnPost.Name = "btnPost";
            this.btnPost.Size = new System.Drawing.Size(202, 23);
            this.btnPost.TabIndex = 2;
            this.btnPost.Text = "Viết/Sửa/Xóa/Xem bài báo";
            this.btnPost.UseVisualStyleBackColor = true;
            this.btnPost.Click += new System.EventHandler(this.btnPost_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(480, 103);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(202, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "Đăng xuất";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnEditProfile
            // 
            this.btnEditProfile.Location = new System.Drawing.Point(33, 148);
            this.btnEditProfile.Name = "btnEditProfile";
            this.btnEditProfile.Size = new System.Drawing.Size(202, 23);
            this.btnEditProfile.TabIndex = 4;
            this.btnEditProfile.Text = "Sửa thông tin cá nhân";
            this.btnEditProfile.UseVisualStyleBackColor = true;
            this.btnEditProfile.Click += new System.EventHandler(this.btnEditProfile_Click);
            // 
            // FormAuthor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 317);
            this.Controls.Add(this.btnEditProfile);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnPost);
            this.Controls.Add(this.btnProfile);
            this.Controls.Add(this.label1);
            this.Name = "FormAuthor";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnProfile;
        private System.Windows.Forms.Button btnPost;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnEditProfile;
    }
}