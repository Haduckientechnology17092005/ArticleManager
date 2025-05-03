namespace WindowsFormsApp1.Presentation
{
    partial class FormAdmin
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
            this.btnUser = new System.Windows.Forms.Button();
            this.btnPostManager = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(176, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(252, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Chào mừng bạn đến  với trang chủ dành cho Admin";
            // 
            // btnUser
            // 
            this.btnUser.Location = new System.Drawing.Point(23, 149);
            this.btnUser.Name = "btnUser";
            this.btnUser.Size = new System.Drawing.Size(149, 23);
            this.btnUser.TabIndex = 1;
            this.btnUser.Text = "Quản lý người dùng";
            this.btnUser.UseVisualStyleBackColor = true;
            this.btnUser.Click += new System.EventHandler(this.btnUser_Click);
            // 
            // btnPostManager
            // 
            this.btnPostManager.Location = new System.Drawing.Point(220, 149);
            this.btnPostManager.Name = "btnPostManager";
            this.btnPostManager.Size = new System.Drawing.Size(149, 23);
            this.btnPostManager.TabIndex = 2;
            this.btnPostManager.Text = "Quản lý bài báo";
            this.btnPostManager.UseVisualStyleBackColor = true;
            this.btnPostManager.Click += new System.EventHandler(this.btnPostManager_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(431, 149);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(149, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "Đăng xuất";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // FormAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 256);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnPostManager);
            this.Controls.Add(this.btnUser);
            this.Controls.Add(this.label1);
            this.Name = "FormAdmin";
            this.Text = "FormAdmin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnUser;
        private System.Windows.Forms.Button btnPostManager;
        private System.Windows.Forms.Button button3;
    }
}